using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GrpcWpfTestServer.Services;
using System.Collections.ObjectModel;

namespace GrpcWpfTestServer
{
	[INotifyPropertyChanged]
	public partial class MainWIndowViewModel
    {
		
        public MainWIndowViewModel()
        {
	
		}


		[RelayCommand]
		private void Operation()
		{
			WeakReferenceMessenger.Default.Send(new OperationMessage { Name = "TestName" , Command = "TestCommand"});
		}
	}
}
