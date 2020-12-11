using System;
using System.Collections.ObjectModel;
using System.Linq;
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
			if (CurrentUser == null || _client == null)
				return;

			if (_client.State == CommunicationState.Faulted)
			{
				_client.Abort();
				_client = null;
			}
			else
			{
				var user = new TcpServiceReference.User { Id = CurrentUser.Id, Name = CurrentUser.Name };
				_client.Disconnect(user);
			}
		}

		~MainWindowViewModel()
		{
			Dispose();
		}

		private TcpServiceReference.ChatClient _client;

		public ObservableCollection<MessageModel> Messages { get; set; } = new ObservableCollection<MessageModel>();

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
				OnPropertyChanged(nameof(UserName));
			}
		}

		private string _notification;
		public string Notification
		{
			get
			{
				return _notification;
			}
			set
			{
				_notification = value;
				OnPropertyChanged(nameof(Notification));
			}
		}

		private bool _isUserNameEnabled = true;
		public bool IsUserNameEnabled
		{
			get
			{
				return _isUserNameEnabled;
			}
			set
			{
				_isUserNameEnabled = value;
				OnPropertyChanged(nameof(IsUserNameEnabled));
			}
		}

		#region Commands

		public ICommand EnterName { get => new RelayCommand(OnEnterName); }
		public ICommand EnterMessage { get => new RelayCommand(OnEnterMessage); }
		public ICommand Connect { get => new RelayCommand(OnConnect); }
		public ICommand Send { get => new RelayCommand(OnSend); }

		#endregion

		private void OnEnterName()
		{
			IsUserNameEnabled = false;
		}

		private void OnEnterMessage()
		{
			OnSend();
		}

		private void OnConnect()
		{
			var users = _client.GetConnectedUsers().ToList();
			if(users.Where(u => u.Name == CurrentUser?.Name).FirstOrDefault() != null)
			{
				Notification = $"{CurrentUser.Name} is already connected.";
				return;
			}

			CurrentUser = new UserModel(Generator.GenerateId(), UserName);
			TcpServiceReference.User user = new TcpServiceReference.User { Id = CurrentUser.Id, Name = CurrentUser.Name };
			_client.Connect(user);
		}

		private void OnSend()
		{
			var message = new TcpServiceReference.Message { Content = MessageText, DateTime = DateTime.Now, UserName = UserName };
			_client.Send(message);
			MessageText = string.Empty;
		}

		public void RefreshUsers(TcpServiceReference.User[] users)
		{
		}

		public void Receive(TcpServiceReference.Message msg)
		{
			var message = new MessageModel(msg.Content, msg.DateTime, msg.UserName);
			Application.Current.Dispatcher.Invoke(() => { Messages.Add(message); });
		}

		public void UserConnect(TcpServiceReference.User user)
		{
			Notification = $"{DateTime.Now:HH:mm}: {user.Name} has just enter this chat.";
		}

		public void UserDisconnect(TcpServiceReference.User user)
		{
		}
	}
}
