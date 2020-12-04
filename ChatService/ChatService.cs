using System;
using System.Collections.Generic;

namespace ChatService
{
	public class ChatService : IChat
	{
		private readonly List<User> _allUsers = new List<User>();
		private readonly List<Message> _allMessages = new List<Message>();

		public bool Connect(User user)
		{
			return false;
		}

		public bool Disconnect(User user)
		{
			return false;
		}

		public List<Message> GetMessages()
		{
			return new List<Message>();
		}

		public List<User> GetUsers()
		{
			return new List<User>();
		}

		public string Send(Message message)
		{
			return string.Empty;
		}
	}
}
