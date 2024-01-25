using Grpc.Core;
using GrpcWpfTest.Common;

namespace GrpcWpfTestClient
{
	internal class Program
	{
		static async Task Main(string[] args)
		{
			/*
			Console.WriteLine("Hello, World!");

			using var channel = GrpcChannel.ForAddress("https://localhost:5001");

			channel.ConnectAsync().Wait();

			var client = new Greeter.GreeterClient(channel);
			
			var reply = await client.SayHelloAsync(new HelloRequest
			{
				Name = "Jeya",
			});

			Console.WriteLine("Greeting : " + reply.Message);

			*/

			//string moduleID = "1";
			
			var mailboxName = GetDriverboxName(args);
			object consoleLock = new object();
			using var driverServiceClient = new DriverServiceClient();


			/*
			_ = driverServiceClient.DriverLogs()
			   .ForEachAsync((x) =>
			   {
				   // if the user is writing something, wait until it finishes.
				   lock (consoleLock)
				   {
					   Console.WriteLine($"{x.At.ToDateTime().ToString("HH:mm:ss")} {x.Name}: {x.Content}");
				   }
			   });
			*/
			using (var call = driverServiceClient.m_client.Mailbox(headers: new Metadata { new Metadata.Entry("mailbox-name", mailboxName) }))
			{
				var responseTask = Task.Run(async () =>
				{
					await foreach (var message in call.ResponseStream.ReadAllAsync())
					{
						Console.WriteLine();
						Console.WriteLine($"Name: {message.Name}, Command: {message.Command}, At: {message.At}");
					}
				});
			}

				/*
				using (var call = driverServiceClient.m_client.Subscribe(headers: new Metadata { new Metadata.Entry("mailbox-name", mailboxName) }))
				{
					var responseTask = Task.Run(async () =>
					{
						await foreach (var message in call.ResponseStream.ReadAllAsync())
						{
							Console.WriteLine();
							Console.WriteLine($"Name: {message.Name}, Command: {message.Command}, At: {message.At}");
						}
					});
				}
				*/

				Console.ReadKey();
		}

		static string GetDriverboxName(string[] args)
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
