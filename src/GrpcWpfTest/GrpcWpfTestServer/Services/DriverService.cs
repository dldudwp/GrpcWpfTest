using CommunityToolkit.Mvvm.Messaging;
using Google.Protobuf.WellKnownTypes;

namespace GrpcWpfTestServer.Services
{
	public record SendMessage(string Name, string Command, Timestamp Timestamp);



	public class DriverService : IDriverService
	{
		public event Func<SendMessage, Task>? Send;

		public DriverService()
		{
			WeakReferenceMessenger.Default.Register<OperationMessage>(this, OnOperationMessage);
		}

		private void OnOperationMessage(object recipient, OperationMessage message)
		{
			SendMessage sendMessage = new(message.Name, message.Command, Timestamp.FromDateTime(DateTime.Now.ToUniversalTime()));

			Send?.Invoke(sendMessage);
		}
	}
}
