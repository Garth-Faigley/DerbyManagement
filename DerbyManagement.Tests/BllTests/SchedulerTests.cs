using DerbyManagement.BLL;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DerbyManagement.Tests
{
    [TestFixture]
    class SchedulerTests
    {
        private Scheduler _scheduler;

        [SetUp]
        public void Create_valid_instance()
        {
            _scheduler = new Scheduler();
        }

        // Naming Conventions: 
        // TestCase- MethodName_StateUnderTest
        // Test (individual)- MethodName_StateUnderTest_ExpectedBehavior

        [TestCase(1, 2, 1)]
        [TestCase(150, 2, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 9, 1)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 2, 13)]
        public void ChaoticScheduler_InvalidArrayDimensions(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            try
            {
                var test = _scheduler.ChaoticScheduler(numberOfCars, numberOfLanes, runsPerLane);
                Assert.Fail("Exception not thrown");
            }
            catch (ArgumentOutOfRangeException)
            {
                Assert.Pass();
            }
        }

        // 2-dimensional array- dimension 0 = rows, dimension 1 = columns
        [TestCase(2, 2, 1, 2, 2)]
        [TestCase(2, 4, 1, 2, 4)]
        [TestCase(3, 4, 1, 3, 4)]
        [TestCase(5, 4, 1, 5, 4)]
        [TestCase(9, 4, 1, 9, 4)]
        [TestCase(56, 4, 1, 56, 4)]
        [TestCase(5, 4, 2, 10, 4)]
        [TestCase(7, 4, 3, 21, 4)]
        [TestCase(13, 4, 4, 52, 4)]
        public void ChaoticScheduler_ArrayDimensions(int numberOfCars, int numberOfLanes, int runsPerLane,
            int expectedRows, int expectedColumns)
        {
            var testSchedule = _scheduler.ChaoticScheduler(numberOfCars, numberOfLanes, runsPerLane);

            int rows = testSchedule.GetLength(0);
            int columns = testSchedule.GetLength(1);

            Assert.AreEqual(expectedRows, rows, "Incorrect Rows");
            Assert.AreEqual(expectedColumns, columns, "Incorrect Columns");
        }

        //// Verify each car runs proper number of times in each lane
        //[TestCase(2, 2, 1)]
        //[TestCase(2, 4, 1)]
        //[TestCase(3, 4, 1)]
        ////[TestCase(5, 4, 1)]
        ////[TestCase(9, 4, 1)]
        ////[TestCase(56, 4, 1)]
        ////[TestCase(5, 4, 2)]
        ////[TestCase(7, 4, 3)]
        ////[TestCase(13, 4, 4)]
        //public void PartialPerfectNScheduler_RunsPerLane(int numberOfCars, int numberOfLanes, int runsPerLane)
        //{
        //    var testSchedule = _scheduler.PartialPerfectNScheduler(numberOfCars, numberOfLanes, runsPerLane);

        //    // for each column (Lane) 
        //    for (int thisLane = 0; thisLane < testSchedule.GetLength(1); thisLane++)
        //    {
        //        // for each car
        //        for (int thisCar = 1; thisCar <= numberOfCars; thisCar++)
        //        {
        //            // number of instances of this car in this lane should == runsPerLane
        //            int instancesOfThisCar = 0;

        //            for (int thisHeat = 0; thisHeat < testSchedule.GetLength(0); thisHeat++)
        //            {
        //                if (testSchedule[thisHeat, thisLane] == thisCar)
        //                    instancesOfThisCar++;
        //            }

        //            if (instancesOfThisCar != runsPerLane)
        //                Assert.Fail("Incorrect Runs Per Lane", "Column " + thisLane);
        //        }
        //    }
        //    Assert.Pass();
        //}

        // Verify each car is only in one lane per heat (obviously one car can't race in two lanes at same time) 
        [TestCase(2, 2, 1)]
        [TestCase(2, 4, 1)]
        [TestCase(3, 4, 1)]
        [TestCase(5, 4, 1)]
        [TestCase(9, 4, 1)]
        [TestCase(56, 4, 1)]
        [TestCase(5, 4, 2)]
        [TestCase(7, 4, 3)]
        [TestCase(13, 4, 4)]
        public void PartialPerfectNScheduler_DuplicateCarsPerHeat(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            var testSchedule = _scheduler.ChaoticScheduler(numberOfCars, numberOfLanes, runsPerLane);

            for (int thisHeat = 0; thisHeat < testSchedule.GetLength(0); thisHeat++)
            {
                var carsInThisHeat = new List<int>();
                for (int thisLane = 0; thisLane < testSchedule.GetLength(1); thisLane++)
                {
                    if (testSchedule[thisHeat, thisLane] > 0)
                        carsInThisHeat.Add(testSchedule[thisHeat, thisLane]);
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
