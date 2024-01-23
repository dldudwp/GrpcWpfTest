using Grpc.Core;
using GrpcWpfTest.Common;
using Microsoft.Extensions.Logging;


namespace GrpcWpfTestServer.Services
{
	public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger _logger;

        public GreeterService()
        {
            
        }

		public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
		{
			return Task.FromResult(new HelloReply { Message = "Hello " + request.Name });
		}
	}
}
