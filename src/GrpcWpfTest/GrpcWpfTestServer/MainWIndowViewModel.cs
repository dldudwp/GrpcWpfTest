using CommunityToolkit.Mvvm.ComponentModel;
using GrpcWpfTestServer.Model;
using GrpcWpfTestServer.Services;
using System.Collections.ObjectModel;

namespace GrpcWpfTestServer
{
    [INotifyPropertyChanged]
	public partial class MainWIndowViewModel
    {
		private IDriverService _driverService;

        ObservableCollection<DriverObject> driverObjects = new();

        public MainWIndowViewModel(IDriverService driverService)
        {
			_driverService = driverService;
			_driverService.AddDriverEvent += OnAdddriver;
		}

		private void OnAdddriver(object? sender, EventArgs e)
		{
			var driver = (DriverObject)sender;

			driverObjects.Add(driver);
		}
	}
}
