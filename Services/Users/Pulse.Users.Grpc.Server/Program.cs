using Pulse.Users.Grpc.Server.Services;
using Pulse.Users.Application;
using Pulse.Users.Database;


var builder = WebApplication.CreateBuilder(args);

string connection = builder.Configuration.GetConnectionString("SqlServer");

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddDatabase(connection);
builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<UsersGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client");

app.Run();
