using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Chat.Helpers;

namespace Chat.ViewModels
{
	class MainWindowViewModel : INotifyPropertyChanged
	{
		public MainWindowViewModel()
		{
		}

		public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();

		public string MessageText { get; set; }

		public string UserName { get; set; }

		#region Commands

		public ICommand Connect { get => new RelayCommand(OnConnect); }
		public ICommand Send { get => new RelayCommand(OnSend); }

		#endregion

		private void OnConnect()
		{
		}

		private void OnSend()
		{
		}

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		#endregion
	}
}
