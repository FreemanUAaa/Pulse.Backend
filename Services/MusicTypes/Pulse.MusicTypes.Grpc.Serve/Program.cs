using Pulse.MusicTypes.Grpc.Serve.Services;
using Pulse.MusicTypes.Application;
using Pulse.MusicTypes.Database;
using Serilog;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDatabase(connection);
builder.Services.AddGrpc();


var app = builder.Build();

app.UseSerilogRequestLogging();

app.MapGrpcService<MusicTypesGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

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
