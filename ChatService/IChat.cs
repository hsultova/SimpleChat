using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ChatService
{
	[ServiceContract(CallbackContract = typeof(IChatServiceCallback), SessionMode = SessionMode.Required)]
	public interface IChat
	{
		[OperationContract]
		bool Connect(User user);

		[OperationContract]
		bool Disconnect(User user);

		[OperationContract]
		void Send(Message message);

		[OperationContract]
		List<Message> GetMessages();

		[OperationContract]
		List<User> GetConnectedUsers();

		[OperationContract]
		bool IsUserConnected(string Id);
	}

	public interface IChatServiceCallback
	{
		[OperationContract(IsOneWay = true)]
		void RefreshUsers(List<User> users);

		[OperationContract(IsOneWay = true)]
		void Receive(Message msg);

		[OperationContract(IsOneWay = true)]
		void UserConnect(User user);

		[OperationContract(IsOneWay = true)]
		void UserDisconnect(User user);
	}

	[DataContract]
	public class User
	{
		[DataMember]
		public string Id { get; set; }

		[DataMember]
		public string Name { get; set; }
	}

	[DataContract]
	public class Message
	{
		[DataMember]
		public string Content { get; set; }

		[DataMember]
		public string UserName { get; set; }

		[DataMember]
		public DateTime DateTime { get; set; }
	}
}
