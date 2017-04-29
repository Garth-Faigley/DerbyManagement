using DerbyManagement.BLL;
using NUnit.Framework;
using System;
using System.Linq;

namespace DerbyManagement.Tests.BllTests
{
    [TestFixture]
    class EnhancedSchedulerTests
    {
        private IScheduler _enhancedRaceScheduler;

        [SetUp]
        public void SetUp()
        {
            SchedulerDetails[] schedulerDetails = RaceScheduler.GetAllSchedulers();
            _enhancedRaceScheduler = RaceScheduler.GetScheduler(schedulerDetails.Where(q => q.ClassName.Equals("EnhancedLaneRotation")).First());
        }

        // Naming Conventions: 
        // TestCase- MethodName_StateUnderTest
        // Test (individual)- MethodName_StateUnderTest_ExpectedBehavior

        [TestCase(1, 2, 1)]
        [TestCase(201, 2, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 9, 1)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 2, 13)]
        public void EnhancedScheduler_InvalidInputData(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            try
            {
                var raceSchedule = _enhancedRaceScheduler.GetRaceSchedule(numberOfLanes, numberOfCars, runsPerLane, true);
                var heats = raceSchedule.Heats.ToList();
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Pass();
            }
        }

        // 2-dimensional array- dimension 0 = rows, dimension 1 = columns
        [TestCase(2, 2, 1, 2, 2)]
        [TestCase(4, 4, 1, 4, 4)]
        [TestCase(5, 4, 1, 5, 4)]
        [TestCase(7, 4, 1, 7, 4)]
        [TestCase(9, 4, 1, 9, 4)]
        [TestCase(56, 4, 1, 56, 4)]
        [TestCase(6, 6, 2, 12, 6)]
        public void EnhancedScheduler_ArrayDimensions(int numberOfCars, int numberOfLanes, int runsPerLane,
            int expectedRows, int expectedColumns)
        {
            var raceSchedule = _enhancedRaceScheduler.GetRaceSchedule(numberOfLanes, numberOfCars, runsPerLane, true);
            var heats = raceSchedule.Heats.ToList();

            int rows = heats.Count;
            int columns = heats[0].ToList().Count;

            Assert.AreEqual(expectedRows, rows, "Incorrect Rows");
            Assert.AreEqual(expectedColumns, columns, "Incorrect Columns");
        }

    }
}
