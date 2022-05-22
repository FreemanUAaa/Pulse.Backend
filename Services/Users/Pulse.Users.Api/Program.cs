using Pulse.Users.Application;
using Pulse.Users.Producers;
using Pulse.Users.Database;
using Pulse.Users.Application.Mapper;
using Pulse.Users.Core.Interfaces.Mapper;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


string connection = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDatabase(connection);
builder.Services.AddProducers();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(typeof(DatabaseContext).Assembly));
    config.AddProfile(new AssemblyMappingProfile(typeof(IMapWith<>).Assembly));
    config.AddProfile(new AssemblyMappingProfile(typeof(AssemblyMappingProfile).Assembly));
});

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowedAll", opt =>
    {
        opt.AllowAnyMethod();
        opt.AllowAnyHeader();
        opt.AllowAnyOrigin();
    });
});

builder.Services.AddApiVersioning();

builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "User Service",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.AddApplicationMiddlewares();

app.UseHttpsRedirection();

app.UseCors("AllowedAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (IServiceScope scope = app.Services.CreateScope())
{
    try
    {
        DatabaseContext context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
        DatabaseInitializator.Initializat(context);
    }
    catch
    {
        //Log.Fatal("Fail to create database");
        throw new Exception("Fail to create database");
    }
}

app.Run();
