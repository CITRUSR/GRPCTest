using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace GRPCTest.Services;

public class TestService : GRPCTest.TestService.TestServiceBase
{
    public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
    {
        HelloReply reply = new HelloReply();
        reply.Text = request.Text + "C#" + " = LOVE";

        return Task.FromResult(reply);
    }

    public override Task<Empty> SayException(Empty request, ServerCallContext context)
    {
        throw new RpcException(new Status(StatusCode.NotFound, "Go is not found"));
    }
}