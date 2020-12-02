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

		#region INotifyPropertyChanged

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged([CallerMemberName] string propertyName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

		#endregion

		public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();

		private string _messageText;
		public string MessageText
		{
			get
			{
				return _messageText;
			}
			set
			{
				_messageText = value;
				OnPropertyChanged(nameof(MessageText));
			}
		}


		private string _userName;
		public string UserName
		{
			get
			{
				return _userName;
			}
			set
			{
				_userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}

		#region Commands

		public ICommand EnterName { get => new RelayCommand(OnEnterName); }

		public ICommand Connect { get => new RelayCommand(OnConnect); }
		public ICommand Send { get => new RelayCommand(OnSend); }

		#endregion


		private void OnEnterName()
		{
		}

		private void OnConnect()
		{
		}

		private void OnSend()
		{
		}
	}
}
