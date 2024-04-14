using GRPCTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<TestService>();
app.MapGet("/",
    () =>
        "C# GRPC SERVICE");

app.Run();