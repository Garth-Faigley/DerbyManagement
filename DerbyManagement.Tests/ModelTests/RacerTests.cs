using DerbyManagement.Model;
using NUnit.Framework;
using System.Collections.Generic;

namespace DerbyManagement.Tests
{
    [TestFixture]
    class RacerTests
    {
        private Racer _racer; // subject under test

        [SetUp]
        public void Create_valid_instance()
        {
            Division testDivision = new Division
            {
                DivisionId = 1,
                Name = "Test Division"
            };

            _racer = new Racer
            {
                RacerId = 42,
                CarNumber = 56,
                CarName = "Test Car Name",
                OwnerFirstName = "FirstName",
                OwnerLastName = "LastName",
                Divisions = new List<Division> { testDivision }
            };
        }

        // Naming Conventions: 
        // TestCase- MethodName_StateUnderTest
        // Test (individual)- MethodName_StateUnderTest_ExpectedBehavior

        [TestCase(7, "Car Number must be between 10 and 200")]
        [TestCase(201, "Car Number must be between 10 and 200")]
        [TestCase(42, "")]
        public void Indexer_CarNumberMustBeBetween10And200(int carNumber, string expectedResult)
        {
            _racer.CarNumber = carNumber;
            Assert.AreEqual(expectedResult, _racer["CarNumber"]);
        }

        [TestCase(null, "The CarName field is required.")]
        [TestCase("", "The CarName field is required.")]
        [TestCase("    ", "The CarName field is required.")]
        [TestCase("Test", "")]
        public void Indexer_CarNameRequired(string carName, string expectedResult)
        {
            _racer.CarName = carName;
            Assert.AreEqual(expectedResult, _racer["CarName"]);
        }

        [Test]
        public void Indexer_CarNameOver255Characters_ValidationFails()
        {
            _racer.CarName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Assert.AreEqual("Car Name maximum length is 255 characters", _racer["CarName"]);
        }

        [TestCase(null, "The OwnerFirstName field is required.")]
        [TestCase("", "The OwnerFirstName field is required.")]
        [TestCase("    ", "The OwnerFirstName field is required.")]
        [TestCase("Test", "")]
        public void Indexer_OwnerFirstName(string ownerFirstName, string expectedResult)
        {
            _racer.OwnerFirstName = ownerFirstName;
            Assert.AreEqual(expectedResult, _racer["OwnerFirstName"]);
        }

        [Test]
        public void Indexer_OwnerFirstNameOver255Characters_ValidationFails()
        {
            _racer.OwnerFirstName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Assert.AreEqual("Owner First Name maximum length is 255 characters", _racer["OwnerFirstName"]);
        }


        [TestCase(null, "The OwnerLastName field is required.")]
        [TestCase("", "The OwnerLastName field is required.")]
        [TestCase("    ", "The OwnerLastName field is required.")]
        [TestCase("Test", "")]
        public void Indexer_OwnerLastName(string ownerLastName, string expectedResult)
        {
            _racer.OwnerLastName = ownerLastName;
            Assert.AreEqual(expectedResult, _racer["OwnerLastName"]);
        }

        [Test]
        public void Indexer_OwnerLastNameOver255Characters_ValidationFails()
        {
            _racer.OwnerLastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            Assert.AreEqual("Owner Last Name maximum length is 255 characters", _racer["OwnerLastName"]);
        }

        [Test]
        public void Indexer_NoDivisions_ValidationFails()
        {
            _racer.Divisions.RemoveAt(0);
            Assert.AreEqual("At least one Division is required", _racer["Divisions"]);
        }

        [Test]
        public void Indexer_OneDivisions_ValidationPasses()
        {
            Assert.AreEqual(string.Empty, _racer["Divisions"]);
        }

    }
}
