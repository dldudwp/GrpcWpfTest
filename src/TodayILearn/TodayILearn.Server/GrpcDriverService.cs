using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayILearn.Common;

namespace TodayILearn.Server
{
    class GrpcDriverService : Driver.DriverBase
    {
		private DriverSevice _driverSevice;

		public GrpcDriverService(DriverSevice driverSevice)
        {
			_driverSevice = driverSevice;
		}

        public override async Task Mailbox(IAsyncStreamReader<DriverLog> requestStream, IServerStreamWriter<ServerMessage> responseStream, ServerCallContext context)
		{
			var mailboxName = context.RequestHeaders.Single(e => e.Key == "mailbox-name").Value;

			_driverSevice.Send += SendMessageing;

			try
			{
				while (await requestStream.MoveNext())
				{
					_driverSevice.ReceivedMessage(mailboxName,requestStream.Current);
				}
			}
			catch (Exception)
			{

				throw;
			}

			async Task SendMessageing(SendMessage message)
			{
				if(mailboxName.Equals(message.Name))
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
}
