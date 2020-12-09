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
			_user = new User();
			_message = new Message();
		}

		[Test]
		public void ConnectTest()
		{
			//Arrange
			var operationContexWrapperFake = Substitute.For<IOperationContext>();
			operationContexWrapperFake.GetCallbackChannel<IChatServiceCallback>().Returns(Substitute.For<IChatServiceCallback>());
			_chatService.OperationContextWrapper = operationContexWrapperFake;

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

		//[Test]
		//public void SendTest()
		//{
		//	_service.Send(_message);
		//}
	}
}
