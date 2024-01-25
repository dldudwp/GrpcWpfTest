using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcWpfTest.Common;

namespace GrpcWpfTestServer.Services
{
    public class DriverGrpcService : Driver.DriverBase
    {
		public override async Task Mailbox(IAsyncStreamReader<DriverLog> requestStream, IServerStreamWriter<ServerMessage> responseStream, ServerCallContext context)
		{
			var peer = context.Peer; // keep peer information because it is not available after disconnection

			//_driverService.Send += SendMessageing;

			try
			{
				while (await requestStream.MoveNext())
				{
					
				}
			}
			catch (TaskCanceledException)
			{
                
            }

			async Task SendMessageing(SendMessage message )
			{
				await responseStream.WriteAsync(new ServerMessage
				{
					Name = message.Name,
					Command = message.Command,
					At = message.Timestamp
				});
			}
		}
	}
}