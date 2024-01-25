using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayILearn.Server
{
	[INotifyPropertyChanged]
    public partial class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            
        }

		[RelayCommand]
		private void Operation()
		{
			WeakReferenceMessenger.Default.Send(new OperationMessage { Name = "TestName", Command = "TestCommand" });
		}
	}
}
