using DerbyManagement.App.Services;
using DerbyManagement.App.ViewModels;
using DerbyManagement.Model;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace DerbyManagement.Tests
{
    [TestFixture]
    class RacerDetailViewModelTests
    {
        [TestCase(42, "This car number is already taken.  Please select another number.")]
        [TestCase(43, "")]
        public void Indexer_DuplicateCarNumberCheck(int carNumber, string expectedResult)
        {
            // Arrange 
            var mockDivision = new Mock<Division>();

            var mockIDerbyDataService = new Mock<IDerbyDataService>();
            mockIDerbyDataService
                .Setup(x => x.CheckCarNumberUnique(It.IsAny<int>(), It.IsAny<int>(), It.Is<int>(y => y == 42)))
                .Returns(1);
            mockIDerbyDataService
                .Setup(x => x.CheckCarNumberUnique(It.IsAny<int>(), It.IsAny<int>(), It.Is<int>(y => y != 42)))
                .Returns(0);
            mockIDerbyDataService
                .Setup(y => y.GetCurrentDerby())
                .Returns(new Mock<Derby>().Object);
            mockIDerbyDataService
                .Setup(y => y.GetAllDivisionsExceptChampionship(It.IsAny<int>()))
                .Returns(new List<Division> { mockDivision.Object });

            var mockRacer = Mock.Of<Racer>(r => r.CarNumber == carNumber);

            var racerDetailViewModel = new RacerDetailViewModel(mockIDerbyDataService.Object);

            // Act
            racerDetailViewModel.LoadRacer(mockRacer);

            // Assert
            Assert.AreEqual(expectedResult, racerDetailViewModel["CarNumber"]);
        }

    }
}
