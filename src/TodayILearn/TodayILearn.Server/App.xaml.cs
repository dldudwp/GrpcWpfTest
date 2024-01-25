using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Net;
using System.Windows;

namespace TodayILearn.Server
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        public App()
        {
			m_serviceProvider = ServiceConfigure();
		}

		private IServiceProvider? ServiceConfigure()
		{
			var services = new ServiceCollection();

			services.AddSingleton<IDriverService, DriverSevice>();
			services.AddSingleton<MainWindowViewModel>();

			return services.BuildServiceProvider();
		}

		private IServiceProvider m_serviceProvider;
		private WebApplication m_app;

        protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);


			var builder = WebApplication.CreateBuilder();

			builder.WebHost.UseUrls("https://localhost:50052");
			// Add services to the container.
			builder.Services.AddGrpc();
			builder.Services.AddSingleton<DriverSevice>();

			m_app = builder.Build();

			m_app.MapGrpcService<GrpcDriverService>();

			m_app.RunAsync();




		}
	}

	
}
