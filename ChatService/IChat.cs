using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatService
{
	[ServiceContract]
	public interface IChat
	{
		[OperationContract]
		bool Connect(User user);

		[OperationContract]
		bool Disconnect(User user);

		[OperationContract]
		bool Send(Message message);

		[OperationContract]
		List<Message> GetMessages();

		[OperationContract]
		List<User> GetConnectedUsers();

		[OperationContract]
		bool IsUserConnected(int Id);
	}

	[DataContract]
	public class User
	{
		int id;
		string name = string.Empty;

		[DataMember]
		public int Id
		{
			get { return id; }
			set { id = value; }
		}

		[DataMember]
		public string Name
		{
			get { return name; }
			set { name = value; }
		}
	}

	[DataContract]
	public class Message
	{
		string content = string.Empty;
		string userName;
		DateTime dateTime;

		[DataMember]
		public string Content
		{
			get { return content; }
			set { content = value; }
		}

		[DataMember]
		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		[DataMember]
		public DateTime DateTime
		{
			get { return dateTime; }
			set { dateTime = value; }
		}
	}
}
