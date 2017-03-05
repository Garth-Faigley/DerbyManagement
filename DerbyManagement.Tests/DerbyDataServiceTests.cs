using DerbyManagement.DAL;
using DerbyManagement.Tests.Mocks;
using NUnit.Framework;
using DerbyManagement.App.Services;

namespace DerbyManagement.Tests
{
    [TestFixture]
    public class DerbyDataServiceTests
    {
        private IDerbyRepository repository;

        [SetUp]
        public void Init()
        {
            repository = new MockRepository();
        }

        [Test]
        public void GetRacerDetailTest()
        {
            //arrange
            var service = new DerbyDataService(repository);

            //act
            var derby = service.GetCurrentDerbyWithDivisions();

            //assert
            Assert.IsNotNull(derby);
        }

    }
}
