using Chat.Helpers;
using NUnit.Framework;

namespace Chat.Tests
{
	[TestFixture]
	public class GeneratorTests
	{
		[Test]
		public void GenerateIdTest()
		{
			//Arrange

			//Act
			string id1 = Generator.GenerateId();
			string id2 = Generator.GenerateId();

			//Assert
			Assert.That(id1 != id2);
		}
	}
}
