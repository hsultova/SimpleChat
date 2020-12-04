using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatService
{
	public class ChatService : IChat
	{
		private readonly List<User> _connectedUsers = new List<User>();
		private readonly List<Message> _allMessages = new List<Message>();

		virtual public bool Connect(User user)
		{
			if (user != null && !_connectedUsers.Contains(user))
			{
				_connectedUsers.Add(user);
				return true;
			}

			return false;
		}

		public bool Disconnect(User user)
		{
			if (user != null)
			{
				_connectedUsers.Remove(user);
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
			return _connectedUsers;
		}

		public bool Send(Message message)
		{
			_allMessages.Add(message);
			return true;
		}

		virtual public bool IsUserConnected(int Id)
		{
			var user = GetConnectedUsers().Where(u => u.Id == Id).FirstOrDefault();
			return user != null;
		}
	}
}
