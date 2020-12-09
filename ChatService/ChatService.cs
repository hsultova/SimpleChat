using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChatService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class ChatService : IChat
	{
		private readonly Dictionary<User, IChatServiceCallback> _connectedUsers = new Dictionary<User, IChatServiceCallback>();
		private readonly List<Message> _allMessages = new List<Message>();

		public IOperationContext OperationContextWrapper = new OperationContextWrapper();

		virtual public bool Connect(User user)
		{
			if (user != null && !_connectedUsers.ContainsKey(user))
			{
				IChatServiceCallback callback = OperationContextWrapper.GetCallbackChannel<IChatServiceCallback>();
				_connectedUsers.Add(user, callback);
				callback.RefreshUsers(_connectedUsers.Keys.ToList());
				callback.UserConnect(user);
				return true;
			}

			return false;
		}

		public bool Disconnect(User user)
		{
			if (user != null)
			{
				_connectedUsers.TryGetValue(user, out IChatServiceCallback callback);
				_connectedUsers.Remove(user);
				callback.UserDisconnect(user);
				return true;
			}

			return false;
		}

		public List<Message> GetMessages()
		{
			return _allMessages;
		}

		virtual public List<User> GetConnectedUsers()
		{
			return _connectedUsers.Keys.ToList();
		}

		public void Send(Message message)
		{
			_allMessages.Add(message);
			foreach (User key in _connectedUsers.Keys.ToList())
			{
				IChatServiceCallback callback = _connectedUsers[key];
				callback.Receive(message);
			}
		}

		virtual public bool IsUserConnected(string Id)
		{
			var user = GetConnectedUsers().Where(u => u.Id == Id).FirstOrDefault();
			return user != null;
		}
	}
}
