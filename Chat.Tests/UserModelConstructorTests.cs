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
            int id = 0;

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
            int id = 0;

            //Act
            var user = new UserModel(0, name);

            //Assert
            Assert.That(user.Id.Equals(id));
            Assert.That(user.Name.Equals("Anonymous"));
        }
    }
}
