using System.Collections.Generic;
using System.Linq;

// Downloaded from:  https://github.com/RyanJean/DerbyTime/tree/master/DerbyRaceScheduler

namespace DerbyManagement.BLL.Schedulers
{
    class BasicLaneRotation : IScheduler
    {
        public SchedulerDetails Details
        {
            get
            {
                return new SchedulerDetails(GetType().Name, "Enhanced Lane Rotation",
                "This racing schedule supports all lane and car counts, and from 1-12 runs depending on lane/car counts.\r\n\r\nThis is the most fair schedule, and guarantees not only that each car will race once per lane, per round, but also the maximum distribution of opponents in the various races.");
            }
        }

        public RunSetInformation[] GetAvailableRuns(int lanes, int cars)
        {
            Utilities.CheckScheduleInputs(lanes, cars);
            var range = Enumerable.Range(1, 12);
            return range.Select(r => new RunSetInformation(r, null)).ToArray();
        }

        public RaceSchedule GetRaceSchedule(int lanes, int cars, int runs, bool shuffle = false)
        {
            Utilities.CheckScheduleInputs(lanes, cars, runs);
            var range = Enumerable.Range(1, cars);
            range = range.Concat(range);

            List<int[]> Heats = new List<int[]>();
            for (int i = 0; i < runs; i++)
            {
                List<int[]> Run = new List<int[]>();
                for (int j = 0; j < cars; j++)
                    Run.Add(range.Skip(j).Take(lanes).ToArray());
                if (shuffle) Run.Shuffle();
                Heats.AddRange(Run.ToArray());
            }
            return new RaceSchedule("Basic Lane Rotation", lanes, cars, runs, Heats.ToArray());
        }
    }
}
