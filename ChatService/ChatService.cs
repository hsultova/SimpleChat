﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ChatService
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
	public class ChatService : IChat
	{
		private readonly Dictionary<string, IChatServiceCallback> _userCallbackPairs = new Dictionary<string, IChatServiceCallback>();
		private readonly List<User> _connectedUsers = new List<User>();
		private readonly List<Message> _allMessages = new List<Message>();

		public IOperationContext OperationContextWrapper = new OperationContextWrapper();

		object syncObj = new object();

		virtual public bool Connect(User user)
		{
			if (user == null || _connectedUsers.Where(u => u.Id == user.Id).FirstOrDefault() != null)
				return false;

			IChatServiceCallback currentCallback = OperationContextWrapper.GetCallbackChannel<IChatServiceCallback>();
			if (currentCallback == null)
				return false;

			lock (syncObj)
			{
				_userCallbackPairs.Add(user.Id, currentCallback);
				_connectedUsers.Add(user);
				foreach (IChatServiceCallback callback in _userCallbackPairs.Values)
				{
					callback.RefreshUsers(_connectedUsers);
					callback.UserConnect(user);
				}
			}
			return true;
		}

		public bool Disconnect(User user)
		{
			if (user == null)
				return false;

			lock(syncObj)
			{
				_userCallbackPairs.Remove(user.Id);
				_connectedUsers.RemoveAll(u => u.Id == user.Id);

				foreach (IChatServiceCallback callback in _userCallbackPairs.Values)
				{
					callback.RefreshUsers(_connectedUsers);
					callback.UserDisconnect(user);
				}
			}

			return true;
		}

		public List<Message> GetMessages()
		{
			return _allMessages;
		}

		virtual public List<User> GetConnectedUsers()
		{
			return _connectedUsers;
		}

		public void Send(Message message)
		{
			lock(syncObj)
			{
				_allMessages.Add(message);
				foreach (User user in _connectedUsers)
				{
					if (_userCallbackPairs.TryGetValue(user.Id, out IChatServiceCallback callback))
						callback?.Receive(message);
				}
			}
		}

		virtual public bool IsUserConnected(string Id)
		{
			var user = GetConnectedUsers().Where(u => u.Id == Id).FirstOrDefault();
			return user != null;
		}
	}
}
