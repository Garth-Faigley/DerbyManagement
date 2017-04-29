using DerbyManagement.BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DerbyManagement.Tests
{
    [TestFixture]
    class ChaoticSchedulerTests
    {
        private IScheduler _chaoticRaceScheduler;

        [SetUp]
        public void SetUp()
        {
             SchedulerDetails[] schedulerDetails = RaceScheduler.GetAllSchedulers();
            _chaoticRaceScheduler = RaceScheduler.GetScheduler(schedulerDetails.Where(q => q.ClassName.Equals("ChaoticLaneRotation")).First());
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
        public void ChaoticScheduler_InvalidInputData(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            try
            {
                var raceSchedule = _chaoticRaceScheduler.GetRaceSchedule(numberOfLanes, numberOfCars, runsPerLane, true);
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
        [TestCase(7, 4, 3, 21, 4)]
        [TestCase(13, 4, 4, 52, 4)]
        public void ChaoticScheduler_ArrayDimensions(int numberOfCars, int numberOfLanes, int runsPerLane,
            int expectedRows, int expectedColumns)
        {
            var raceSchedule = _chaoticRaceScheduler.GetRaceSchedule(numberOfLanes, numberOfCars, runsPerLane, true);
            var heats = raceSchedule.Heats.ToList();

            int rows = heats.Count;
            int columns = heats[0].ToList().Count;

            Assert.AreEqual(expectedRows, rows, "Incorrect Rows");
            Assert.AreEqual(expectedColumns, columns, "Incorrect Columns");
        }

        // Verify each car runs proper number of times in each lane
        [TestCase(2, 2, 1)]
        [TestCase(5, 2, 1)]
        [TestCase(3, 2, 5)]
        [TestCase(4, 4, 1)]
        [TestCase(8, 4, 2)]
        [TestCase(5, 4, 1)]
        [TestCase(9, 4, 1)]
        [TestCase(9, 5, 2)]
        [TestCase(56, 4, 1)]
        [TestCase(5, 4, 2)]
        [TestCase(7, 4, 3)]
        [TestCase(13, 4, 4)]
        public void ChaoticScheduler_RunsPerLane(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            var raceSchedule = _chaoticRaceScheduler.GetRaceSchedule(numberOfLanes, numberOfCars, runsPerLane, true);
            var heats = raceSchedule.Heats.ToList();

            // iterate through each lane
            for (int thisLane = 0; thisLane < numberOfLanes; thisLane++)
            {
                // iterate through each car
                for (int thisCar = 1; thisCar <= numberOfCars; thisCar++)
                {
                    // number of instances of this car in this lane should == runsPerLane
                    int instancesOfThisCar = 0;

                    for (int thisHeat = 0; thisHeat < heats.Count; thisHeat++)
                    {
                        if (heats[thisHeat][thisLane] == thisCar)
                            instancesOfThisCar++;
                    }

                    if (instancesOfThisCar != runsPerLane)
                        Assert.Fail("Incorrect Runs Per Lane", "Column " + thisLane);
                }
            }
            Assert.Pass();
        }

        // Verify each car is only in one lane per heat (obviously one car can't race in two lanes at same time) 
        [TestCase(2, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(6, 6, 1)]
        [TestCase(5, 4, 1)]
        [TestCase(9, 4, 1)]
        [TestCase(56, 4, 1)]
        [TestCase(5, 4, 2)]
        [TestCase(7, 4, 3)]
        [TestCase(13, 4, 4)]
        [TestCase(11, 6, 2)]
        public void ChaoticScheduler_DuplicateCarsPerHeat(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            var raceSchedule = _chaoticRaceScheduler.GetRaceSchedule(numberOfLanes, numberOfCars, runsPerLane, true);
            var heats = raceSchedule.Heats.ToList();

            for (int thisHeat = 0; thisHeat < heats.Count; thisHeat++)
            {
                var carsInThisHeat = new List<int>();
                for (int thisLane = 0; thisLane < heats[0].ToList().Count; thisLane++)
                {
                    if (heats[thisHeat][thisLane] > 0)
                        carsInThisHeat.Add(heats[thisHeat][thisLane]);
                }

                // check for duplicats in the list.  If there are any the test fails.
                var duplicateExists = carsInThisHeat.GroupBy(n => n).Any(g => g.Count() > 1);
                if (duplicateExists)
                    Assert.Fail("Heat has same car in multiple lanes");
            }
            Assert.Pass();
        }

    }
}
