using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace ChatService.Tests
{
	[TestFixture]
	public class ChatServiceTests
	{
		private ChatService _service;
		private ChatService _serviceFake;
		private User _user;
		private Message _message;

		[SetUp]
		public void ChatServiceSetUp()
		{
			_service = new ChatService();
			_serviceFake = Substitute.ForPartsOf<ChatService>();
			_user = new User();
			_message = new Message();
		}

		[Test]
		public void ConnectTest()
		{
			//Arrange
			var isUserConnectedBefore = _service.IsUserConnected(_user.Id);

			//Act
			var isServiceConnected = _service.Connect(_user);
			var isUserConnectedAfter = _service.IsUserConnected(_user.Id);

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
			var isServiceConnected = _service.Connect(user);

			//Assert
			Assert.That(isServiceConnected == false);
		}

		[Test]
		public void IsUserConnectedTest()
		{
			//Arrange
			_serviceFake.GetConnectedUsers().Returns(new List<User> { _user });

			//Act
			var isUserConnected = _serviceFake.IsUserConnected(_user.Id);

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
