using GrpcWpfTestServer.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Windows;

namespace GrpcWpfTestServer
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IServiceProvider _serviceProvider;
		private WebApplication m_app;

		public App()
		{
			_serviceProvider = ServiceConfigure();
		}

		public IServiceProvider ServiceConfigure()
		{
			IServiceCollection services = new ServiceCollection();


			services.AddSingleton<MainWIndowViewModel>();
			services.AddSingleton<IDriverService, DriverService>();

			return services.BuildServiceProvider();
		}


		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var builder = WebApplication.CreateBuilder();

			builder.WebHost.UseUrls("https://localhost:50052");
			// Add services to the container.
			builder.Services.AddGrpc();

			m_app = builder.Build();

			m_app.MapGrpcService<DriverGrpcService>();

			m_app.RunAsync();

			MainWindow win = new();
			win.DataContext = _serviceProvider.GetService<MainWIndowViewModel>();
			win.Show();

		}
	}
}
