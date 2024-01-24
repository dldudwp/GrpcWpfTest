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
		private IServiceProvider _serviceProvider;

		public App()
        {
			_serviceProvider = ServiceConfigure();
		}

		public IServiceProvider ServiceConfigure()
		{
			IServiceCollection services = new ServiceCollection();

			
			services.AddSingleton<IDriverService, DriverService>();


			services.AddSingleton<MainWIndowViewModel>();

			return services.BuildServiceProvider();
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);


			Task.Run(() => StartServer());


			MainWindow win = new();
			win.DataContext = _serviceProvider.GetService<MainWIndowViewModel>();
			win.Show();

		}

		private void StartServer()
		{
			var builder = WebApplication.CreateBuilder();

			builder.WebHost.UseUrls("http://*:5219" , "https://*:50052");

			builder.Services.AddGrpc();

			_app = builder.Build();
			_app.MapGrpcService<DriverGrpcService>();
			_app.Run();
		}
	}

}
