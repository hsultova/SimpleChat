using System;
using Chat.Models;
using NUnit.Framework;

namespace Chat.Tests
{
	[TestFixture]
	public class MessageModelConstructorTests
	{
        [Test]
        public void ConstructorTest()
        {
            //Arrange
            string content = "Content";
            DateTime dateTime = DateTime.Now;
            string name = "TestName";

            //Act
            var message = new MessageModel(content, dateTime, name);

            //Assert
            Assert.That(message.Content.Equals(content));
            Assert.That(message.DateTime.Equals(dateTime));
            Assert.That(message.UserName.Equals(name));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void NoContentConstructorTest(string content)
        {
            //Arrange
            DateTime dateTime = DateTime.Now;
            string name = "TestName";

            //Act
            var message = new MessageModel(content, dateTime, name);

            //Assert
            Assert.That(message.Content.Equals(string.Empty));
            Assert.That(message.DateTime.Equals(dateTime));
            Assert.That(message.UserName.Equals(name));
        }
    }
}
