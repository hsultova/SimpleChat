using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.Windows;
using System.Windows.Input;
using Chat.Helpers;
using Chat.Models;
using Chat.ViewModels.Base;

namespace Chat.ViewModels
{
	[CallbackBehavior(UseSynchronizationContext = false, ConcurrencyMode = ConcurrencyMode.Multiple)]
	class MainWindowViewModel : BaseViewModel, IDisposable, TcpServiceReference.IChatCallback
	{
		public MainWindowViewModel()
		{
			InstanceContext instanceContext = new InstanceContext(this);
			_client = new TcpServiceReference.ChatClient(instanceContext);
		}

		public void Dispose()
		{
		}

		private TcpServiceReference.ChatClient _client;

		public ObservableCollection<string> Messages { get; set; } = new ObservableCollection<string>();

		public UserModel CurrentUser { get; set; }

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
				CurrentUser = new UserModel(Generator.GenerateId(), UserName);
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
			var userModel = new UserModel(Generator.GenerateId(), UserName);
			TcpServiceReference.User user = new TcpServiceReference.User { Id = userModel.Id, Name = userModel.Name };
			_client.Connect(user);
		}

		private void OnSend()
		{
			TcpServiceReference.Message message = new TcpServiceReference.Message { Content = MessageText, DateTime = DateTime.Now, UserName = UserName };
			_client.Send(message);
		}

		public void RefreshUsers(TcpServiceReference.User[] users)
		{

		}

		public void Receive(TcpServiceReference.Message msg)
		{
			Application.Current.Dispatcher.Invoke(() => { Messages.Add(msg.Content); });
		}

		public void UserConnect(TcpServiceReference.User user)
		{

		}

		public void UserDisconnect(TcpServiceReference.User user)
		{

		}
	}
}
