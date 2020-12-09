using System;
using Chat.Models;
using NUnit.Framework;

namespace Chat.Tests
{
    [TestFixture]
    public class UserModelConstructorTests
    {

        [Test]
        public void ConstructorTest()
        {
            //Arrange
            string name = "TestName";
            string id = Guid.NewGuid().ToString();

            //Act
            var user = new UserModel(id, name);

            //Assert
            Assert.That(user.Id.Equals(id));
            Assert.That(user.Name.Equals(name));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void NoNameConstructorTest(string name)
        {
            //Arrange
            string id = Guid.NewGuid().ToString();

            //Act
            var user = new UserModel(id, name);

            //Assert
            Assert.That(user.Id.Equals(id));
            Assert.That(user.Name.Equals("Anonymous"));
        }
    }
}
