using DerbyManagement.Model;
using NUnit.Framework;

namespace DerbyManagement.Tests
{
    [TestFixture]
    class DivisionTests
    {
        private Division _division; // subject under test

        [SetUp]
        public void Create_valid_instance()
        {
            _division = new Division
            {
                DivisionId = 1,
                Name = "Test Division"
            };
        }

        [TestCase(null, "The Name field is required.")]
        [TestCase("", "The Name field is required.")]
        [TestCase("    ", "The Name field is required.")]
        [TestCase("Test", "")]
        public void Indexer_NameRequired(string name, string expectedResult)
        {
            _division.Name = name;
            Assert.AreEqual(expectedResult, _division["Name"]);
        }

        [Test]
        public void Indexer_NameOver255Characters_ValidationFails()
        {
            _division.Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Assert.AreEqual("Division Name maximum length is 255 characters", _division["Name"]);
        }

    }
}
