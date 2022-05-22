using Microsoft.OpenApi.Models;
using Pulse.MusicTypes.Application;
using Pulse.MusicTypes.Database;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);


string connection = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDatabase(connection);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.AddApplicationMiddlewares();

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
        Log.Fatal("Fail to create database");
        throw new Exception("Fail to create database");
    }
}

app.Run();