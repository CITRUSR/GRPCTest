using Grpc.Net.Client;
using GRPCTest.Services;
using Onnywrite.Test;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapGrpcService<TestService>();
app.MapGet("/",
    () =>
        "C# GRPC SERVICE");
app.MapGet("/test", async () =>
{
    using var channel = GrpcChannel.ForAddress("http://test:5055", new GrpcChannelOptions
    {
        HttpHandler = new SocketsHttpHandler
        {
            EnableMultipleHttp2Connections = true
        }
    });
    var client = new Adder.AdderClient(channel);
    var call = await client.AddAsync(new AddRequest() { X = "c#", Y = "YES" });

    return call.Result;
});

app.Run();