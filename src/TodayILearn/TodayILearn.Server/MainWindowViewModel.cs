using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayILearn.Server
{
	[INotifyPropertyChanged]
    public partial class MainWindowViewModel
    {
		private readonly IDriverService _driverService;

        public MainWindowViewModel() {}

        public MainWindowViewModel(IDriverService driverService)
        {
			_driverService = driverService;
}

		ObservableCollection<string> Drivers { get; set; }
		ObservableCollection<string> Logs { get; set; }


		[RelayCommand]
		private void Operation()
		{
			WeakReferenceMessenger.Default.Send(new OperationMessage { Name = "2", Command = "TestCommand" });
		}
	}
}
