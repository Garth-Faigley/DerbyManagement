using DerbyManagement.App.Services;
using DerbyManagement.App.ViewModels;
using DerbyManagement.Model;
using Moq;
using NUnit.Framework;

namespace DerbyManagement.Tests
{
    [TestFixture]
    class DivisionDetailViewModelTests
    {

        [TestCase(4, "Sequence number must be unique.  Please select another sequence number.")]
        [TestCase(5, "")]
        public void Indexer_DuplicateSequenceNumberCheck(int sequenceNumber, string expectedResult)
        {
            // Arrange 
            var mockIDerbyDataService = new Mock<IDerbyDataService>();
            mockIDerbyDataService
               .Setup(x => x.CheckSequenceNumberUnique(It.IsAny<int>(), It.IsAny<int>(), It.Is<int>(y => y == 4)))
               .Returns(1);
            mockIDerbyDataService
                .Setup(x => x.CheckSequenceNumberUnique(It.IsAny<int>(), It.IsAny<int>(), It.Is<int>(y => y != 4)))
                .Returns(0);
            mockIDerbyDataService
                .Setup(y => y.GetCurrentDerby())
                .Returns(new Mock<Derby>().Object);

            var mockDivision = Mock.Of<Division>(r => r.Sequence == sequenceNumber);

            var divisionDetailViewModel = new DivisionDetailViewModel(mockIDerbyDataService.Object);

            // Act
            divisionDetailViewModel.LoadDivision(mockDivision);

            // Assert
            Assert.AreEqual(expectedResult, divisionDetailViewModel["Sequence"]);
        }

    }
}
