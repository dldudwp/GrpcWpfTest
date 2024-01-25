using Grpc.Core;
using Grpc.Net.Client;
using GrpcWpfTest.Common;

namespace GrpcWpfTestClient
{
	public class DriverServiceClient : IDisposable
	{
		public readonly Driver.DriverClient m_client;
		private readonly GrpcChannel m_channel;
		private bool disposedValue;

		public DriverServiceClient()
		{
			var https = false;

			if (https)
			{

			}
			else
			{
				m_channel = GrpcChannel.ForAddress("https://localhost:50052");

				m_channel.ConnectAsync().Wait();

				m_client = new Driver.DriverClient(m_channel);
			}
		}


		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					m_channel.Dispose(); // disposes all of active calls.
				}

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}


	}
}
