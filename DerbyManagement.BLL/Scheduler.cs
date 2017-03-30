using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerbyManagement.BLL
{
    public class Scheduler
    {
        private Random _random = new Random();



        // rule out any cars already scheduled in this heat
        // rule out any cars that have already run the maximum number of times in this lane
        // favor cars with fewer matchups against these opponents
        // favor cars that have not been scheduled recently
        public int[,] ChaoticScheduler(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            var schedule = SetupSchedule(numberOfCars, numberOfLanes, runsPerLane);

            //  assigned heats by car, last heat by car
            var assignedHeats = new List<KeyValuePair<int, int>>();

            // Build list of cars 
            var carList = new List<int>();
            for (int i = 0; i < numberOfCars; i++)
            {
                carList.Add(i + 1);
            }

            for (int thisHeat = 0; thisHeat < schedule.GetLength(0); thisHeat++)
            {
                for (int thisLane = 0; thisLane < schedule.GetLength(1); thisLane++)
                {
                    var car = ChooseCar(schedule, thisHeat, thisLane, carList);
                }
            }

            return schedule;
        }


        private int ChooseCar(int[,] schedule, int heat, int lane, List<int> carList)
        {
            var carsInThisLane = new List<int>();
            var carsInThisHeat = new List<int>();
            
            // Build list of all cars in this lane (in previous heats)
            for (int thisHeat = 0; thisHeat < schedule.GetLength(0); thisHeat++)
            {
                if (schedule[thisHeat, lane] > 0)
                    carsInThisLane.Add(schedule[thisHeat, lane]);
            }

            // Build list of all cars in this heat (in other lanes)
            for (int thisLane = 0; thisLane < schedule.GetLength(1); thisLane++)
            {
                if (schedule[heat, thisLane] > 0)
                    carsInThisHeat.Add(schedule[heat, thisLane]);
            }

            // rule out any cars already scheduled in this heat
            carList = carList.Except(carsInThisLane).ToList();

            // Choose a random car from those left and return it.  If no cars left, return zero
            if (carList.Count == 0)
                return 0;

            var r = _random.Next(carList.Count);
            return carList[r];
        }


        //// See http://www.stanpope.net/cyoung2.html
        ////
        //// Perfect-N (PN) Schedule- 
        //// 1. Each car races in each line the same number of times. 
        //// 2. Each car races each opponent the same number of times. 
        //public string[,] PerfectNScheduler(int numberOfCars, int numberOfLanes, int runsPerLane)
        //{
        //    if (numberOfCars < 2 || numberOfCars > 100)
        //        throw new ArgumentOutOfRangeException("Invalid number of cars");
        //    if (numberOfLanes < 2 || numberOfLanes > 8)
        //        throw new ArgumentOutOfRangeException("Invalid number of lanes");
        //    if (runsPerLane < 1 || runsPerLane > 12)
        //        throw new ArgumentOutOfRangeException("Invalid runs per lane");

        //    throw new NotImplementedException();
        //}

        //// Partial Perfect-N (PPN) Schedule- 
        //// 1. Each car races the same number of times in each lane.  IE. Number of heats is a multiple of the number 
        ////    of cars. 
        //// 2. Equality of opposition is optimized, i.e., no head-to-head matchup count exceeds another by more than 1.
        //public int[,] PartialPerfectNScheduler(int numberOfCars, int numberOfLanes, int runsPerLane)
        //{
        //    var schedule = SetupSchedule(numberOfCars, numberOfLanes, runsPerLane);

        //    //// Populate the first column (ie. Lane 1) with each car in order times number of runs
        //    //for (int i = 0; i < runsPerLane; i++)
        //    //{
        //    //    for (int thisCar = 1; thisCar <= numberOfCars; thisCar++)
        //    //    {
        //    //        var thisHeat = ((i * numberOfCars) + thisCar) - 1;
        //    //        schedule[thisHeat, 0] = thisCar;
        //    //    }
        //    //}

        //    //// Iterate through each row (Heat) and populate the other columns (Lanes 2-n)
        //    //for (int thisHeat = 0; thisHeat < schedule.GetLength(0); thisHeat++)
        //    //{
        //    //    for (int thisLane = 1; thisLane < numberOfLanes; thisLane++)
        //    //    {
        //    //        var car = ChooseRacer(schedule, thisHeat, thisLane, numberOfCars, runsPerLane);
        //    //        schedule[thisHeat, thisLane] = car;
        //    //    }
        //    //}

        //    return schedule;
        //}

        /// <summary>
        /// Create the schedule (2-dimensional array).  Dimensions: 0 = rows (ie. Heats), 1 = columns (ie. Lanes)
        /// </summary>
        /// <param name="numberOfCars"></param>
        /// <param name="numberOfLanes"></param>
        /// <param name="runsPerLane"></param>
        /// <returns></returns>
        private int[,] SetupSchedule(int numberOfCars, int numberOfLanes, int runsPerLane)
        {
            if (numberOfCars < 2 || numberOfCars > 100)
                throw new ArgumentOutOfRangeException("Invalid number of cars");
            if (numberOfLanes < 2 || numberOfLanes > 8)
                throw new ArgumentOutOfRangeException("Invalid number of lanes");
            if (runsPerLane < 1 || runsPerLane > 12)
                throw new ArgumentOutOfRangeException("Invalid runs per lane");

            var numberOfHeats = numberOfCars * runsPerLane;
            return new int[numberOfHeats, numberOfLanes];
        }

        //private int ChooseRacer(int[,] schedule, int heat, int lane, int numberOfCars, int runsPerLane)
        //{
        //    // Build list of cars 
        //    var cars = new List<int>();
        //    for (int i = 0; i < numberOfCars; i++)
        //    {
        //        cars.Add(i + 1);
        //    }

        //    // Remove cars from the list that are not elligibe to race in this run/lane because: 

        //    // Already racing in this heat in another lane
        //    for (int thisLane = 0; thisLane < schedule.GetLength(1); thisLane++)
        //    {
        //        var thisCar = schedule[heat, thisLane];
        //        if (thisCar > 0)
        //            cars.Remove(thisCar);
        //    }

        //    // Over the limit for number of runs in this lane
        //    var carsInThisLane = new List<int>();
        //    for (int thisRun = 0; thisRun < schedule.GetLength(0); thisRun++)
        //    {
        //        var car = schedule[thisRun, lane];
        //        if (car > 0)
        //            carsInThisLane.Add(car);
        //    }
        //    var groupedCarsInThisLane = carsInThisLane.GroupBy(i => i);
        //    foreach (var thisGroup in groupedCarsInThisLane)
        //    {
        //        if (thisGroup.Count() >= runsPerLane)
        //            cars.Remove(thisGroup.Key);
        //    }


        //    // Choose a random car from those left and return it.  If no cars left, return zero
        //    if (cars.Count == 0)
        //        return 0;

        //    var r = _random.Next(cars.Count);
        //    return cars[r];
        //}

    }
}
