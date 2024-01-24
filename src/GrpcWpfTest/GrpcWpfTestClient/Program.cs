using Grpc.Net.Client;
using GrpcWpfTest.Common;
using System.Reflection;

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

			string moduleID = "1";

			using var driverServiceClient = new DriverServiceClient();
			driverServiceClient.Start(moduleID);



			Console.ReadKey();
		}
	}
}
