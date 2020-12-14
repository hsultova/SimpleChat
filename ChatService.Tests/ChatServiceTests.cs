using System;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ChatService.Tests
{
	[TestFixture]
	public class ChatServiceTests
	{
		private ChatService _chatService;
		private User _user;
		private Message _message;

		[SetUp]
		public void ChatServiceSetUp()
		{
			_chatService = new ChatService();
			_user = new User { Id = Guid.NewGuid().ToString(), Name = "TestUser" };
			_message = new Message { Content = "Test content", DateTime = DateTime.Now, UserName = "TestUser"};

			//Add fake operation context wrapper with default chat service callback
			var operationContexWrapperFake = Substitute.For<IOperationContext>();
			operationContexWrapperFake.GetCallbackChannel<IChatServiceCallback>().Returns(Substitute.For<IChatServiceCallback>());
			_chatService.OperationContextWrapper = operationContexWrapperFake;
		}

		[Test]
		public void ConnectTest()
		{
			//Arrange
			var isUserConnectedBefore = _chatService.IsUserConnected(_user.Id);

			//Act
			var isServiceConnected = _chatService.Connect(_user);
			var isUserConnectedAfter = _chatService.IsUserConnected(_user.Id);

			//Assert
			Assert.That(isUserConnectedBefore == false);
			Assert.That(isServiceConnected == true);
			Assert.That(isUserConnectedAfter == true);
		}

		[Test]
		public void DisconnectTest()
		{
			//Arrange
			_chatService.Connect(_user);
			var isUserConnectedBefore = _chatService.IsUserConnected(_user.Id);

			//Act
			_chatService.Disconnect(_user);
			var isUserConnectedAfter = _chatService.IsUserConnected(_user.Id);

			//Assert
			Assert.That(isUserConnectedBefore == true);
			Assert.That(isUserConnectedAfter == false);
		}

		[Test]
		[TestCase(null)]
		public void ConnectNoUserTest(User user)
		{
			//Act
			var isServiceConnected = _chatService.Connect(user);

			//Assert
			Assert.That(isServiceConnected == false);
		}

		[Test]
		public void IsUserConnectedTest()
		{
			//Arrange
			ChatService chatServiceFake = Substitute.ForPartsOf<ChatService>();
			chatServiceFake.GetConnectedUsers().Returns(new List<User> { _user });

			//Act
			var isUserConnected = chatServiceFake.IsUserConnected(_user.Id);

			//Assert
			Assert.That(isUserConnected == true);
		}
	}
}
