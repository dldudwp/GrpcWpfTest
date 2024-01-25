using CommunityToolkit.Mvvm.Messaging;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayILearn.Server
{
	public interface IDriverService
	{
		event Func<SendMessage, Task>? Send;
	}

	public record SendMessage(string Name, string Command, Timestamp Timestamp);


	public class DriverSevice : IDriverService
	{

		public event Func<SendMessage, Task>? Send;

		public DriverSevice()
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
