using Grpc.Core;
using Grpc.Net.Client;
using TodayILearn.Common;

namespace TodayILearn
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			var mailboxName = GetMailboxName(args);

			Console.WriteLine($"Creating client to mailbox '{mailboxName}'");
			Console.WriteLine();

			var channel = GrpcChannel.ForAddress("https://localhost:50052");

			channel.ConnectAsync().Wait();	

			var client = new Driver.DriverClient(channel);

			Console.WriteLine("Client created");
			Console.WriteLine("Press escape to disconnect. Press any other key to forward mail.");

			using (var call = client.Mailbox(headers: new Metadata { new Metadata.Entry("mailbox-name", mailboxName) }))
			{
				var responseTask = Task.Run(async () =>
				{
					await foreach (var message in call.ResponseStream.ReadAllAsync())
					{
						Console.WriteLine();
						Console.WriteLine($"Name: {message.Name}, Command: {message.Command}, At: {message.At}");
					}
				});

				while (true)
				{
					var result = Console.ReadKey(intercept: true);
					if (result.Key == ConsoleKey.Escape)
					{
						break;
					}

				}

				Console.WriteLine("Disconnecting");
				await call.RequestStream.CompleteAsync();
				await responseTask;
			}

			Console.WriteLine("Disconnected. Press any key to exit.");
			Console.ReadKey();
		}

		private static string GetMailboxName(string[] args)
		{
			if (args.Length < 1)
			{
				Console.WriteLine("No mailbox name provided. Using default name. Usage: dotnet run <name>.");
				return "DefaultMailbox";
			}

			return args[0];
		}
	}
}
