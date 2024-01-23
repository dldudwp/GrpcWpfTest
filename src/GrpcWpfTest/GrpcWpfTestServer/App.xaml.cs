using GrpcWpfTestServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace GrpcWpfTestServer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private WebApplication _app;

		public App()
        {
            
        }


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			Task.Run(() => StartServer());
		}

		private void StartServer()
		{
			var builder = WebApplication.CreateBuilder();

			builder.WebHost.UseUrls("http://*:5219" , "https://*:5001");

			builder.Services.AddGrpc();

			_app = builder.Build();
			_app.MapGrpcService<GreeterService>();
			_app.Run();
		}
	}

}
