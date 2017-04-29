using System;
using System.Collections.Generic;
using System.Linq;

// Downloaded from:  https://github.com/RyanJean/DerbyTime/tree/master/DerbyRaceScheduler 
// Pretty sure the original idea came from Stan Pope - http://www.stanpope.net/ppngen.html 

namespace DerbyManagement.BLL.Schedulers
{
    public class EnhancedLaneRotation : IScheduler
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
            var runs = GetRuns(lanes, cars);
            var range = Enumerable.Range(1, runs[0]).ToArray();
            return range.Select(r => new RunSetInformation(r, ScheduleTypes[runs[r]])).ToArray();
        }

        public RaceSchedule GetRaceSchedule(int lanes, int cars, int runs, bool shuffle = false)
        {
            Utilities.CheckScheduleInputs(lanes, cars, runs);

            var allRuns = GetRuns(lanes, cars);
            var thisRun = CleanupRuns(allRuns, lanes, runs);

            List<int[]> Heats = new List<int[]>();
            foreach (int[] run in thisRun)
            {
                List<int[]> Run = new List<int[]>();
                for (int j = 0; j < cars; j++)
                    Run.Add(run.Select(q => (q + j) % cars + 1).ToArray());
                if (shuffle) Run.Shuffle();
                Heats.AddRange(Run.ToArray());
            }
            return new RaceSchedule("Enhanced Lane Rotation | " + ScheduleTypes[allRuns[runs]], lanes, cars, runs, Heats.ToArray());
        }

        private string[] ScheduleTypes = { "Miscellaneous", "Partial Perfect-N", "Perfect-N", "Complimentary Perfect-N" };

        #region int[] getRuns(int lanes, int cars)
        private int[] GetRuns(int lanes, int cars)
        {
            // Format is always:
            // Using L = # of Lanes, and N = # of Runs (obtained from array[0])
            //      Array[0] = # of Runs
            //      Array[1] = Run 1's Type
            //      Array[2] = Run 2's Type
            //          ...
            //      Array[N] = Run N's Type
            //      Array[N+1] = Lane 2 modifier for run 1 (we always start at lane 2 because lane 1 is always 0)
            //          ...
            //      Array[N^2-1] = Lane L modifier for run 1 (the last lane of the track)
            //      Array[N^2] = Lane 2 modifier for run 2
            //          ...And So On...

            switch (lanes)
            {
                case 2:
                    if (cars == 2) return new int[] { 2, 3, 3, 1, 1 };
                    if (cars == 3) return new int[] { 2, 2, 3, 2, 1 };
                    if (cars == 4) return new int[] { 2, 1, 1, 3, 2 };
                    if (cars == 5) return new int[] { 4, 1, 2, 1, 3, 3, 4, 2, 1 };
                    if (cars == 6) return new int[] { 2, 1, 1, 2, 5 };
                    if (cars == 7) return new int[] { 6, 1, 1, 2, 1, 1, 3, 3, 2, 1, 4, 5, 6 };
                    if (cars == 8) return new int[] { 3, 1, 1, 1, 3, 2, 1 };
                    if (cars == 9) return new int[] { 8, 1, 1, 1, 2, 1, 1, 1, 3, 4, 3, 2, 1, 5, 6, 7, 8 };
                    if (cars == 10) return new int[] { 4, 1, 1, 1, 1, 4, 3, 2, 1 };
                    if (cars == 11) return new int[] { 10, 1, 1, 1, 1, 2, 1, 1, 1, 1, 3, 5, 4, 3, 2, 1, 6, 7, 8, 9, 10 };
                    if (cars == 12) return new int[] { 5, 1, 1, 1, 1, 1, 5, 4, 3, 2, 1 };
                    if (cars == 13) return new int[] { 12, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 3, 6, 5, 4, 3, 2, 1, 7, 8, 9, 10, 11, 12 };
                    if (cars == 14) return new int[] { 6, 1, 1, 1, 1, 1, 1, 6, 5, 4, 3, 2, 1 };
                    if (cars == 15) return new int[] { 12, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 1, 7, 6, 5, 4, 3, 2, 1, 8, 9, 10, 11, 12 };
                    if (cars == 16) return new int[] { 7, 1, 1, 1, 1, 1, 1, 1, 7, 6, 5, 4, 3, 2, 1 };
                    if (cars == 17) return new int[] { 12, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 1, 8, 7, 6, 5, 4, 3, 2, 1, 9, 10, 11, 12 };
                    if (cars == 18) return new int[] { 8, 1, 1, 1, 1, 1, 1, 1, 1, 8, 7, 6, 5, 4, 3, 2, 1 };
                    if (cars == 19) return new int[] { 12, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 10, 11, 12 };
                    if (cars == 20) return new int[] { 9, 1, 1, 1, 1, 1, 1, 1, 1, 1, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
                    if (cars == 21) return new int[] { 12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 1, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 11, 12 };
                    if (cars == 22) return new int[] { 10, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
                    if (cars == 23) return new int[] { 12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 1, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 12 };
                    if (cars == 24) return new int[] { 11, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
                    if (cars == 25) return new int[] { 12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 2, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
                    if (cars >= 26 && cars <= 200) return new int[] { 12, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };
                    break;
                case 3:
                    if (cars == 3) return new int[] { 2, 2, 3, 2, 2, 1, 1 };
                    if (cars == 4) return new int[] { 2, 2, 3, 3, 3, 1, 1 };
                    if (cars == 5) return new int[] { 4, 1, 2, 1, 3, 2, 2, 1, 1, 3, 3, 4, 4 };
                    if (cars == 6) return new int[] { 2, 1, 1, 2, 3, 5, 2 };
                    if (cars == 7) return new int[] { 2, 2, 3, 2, 4, 5, 3 };
                    if (cars == 8) return new int[] { 2, 1, 1, 2, 5, 3, 4 };
                    if (cars == 9) return new int[] { 2, 1, 1, 2, 3, 3, 5 };
                    if (cars == 10) return new int[] { 2, 1, 1, 2, 7, 4, 5 };
                    if (cars == 11) return new int[] { 2, 1, 1, 2, 3, 3, 7 };
                    if (cars == 12) return new int[] { 1, 1, 2, 3 };
                    if (cars == 13) return new int[] { 4, 1, 2, 1, 3, 3, 9, 7, 11, 10, 4, 6, 2 };
                    if (cars == 14) return new int[] { 1, 1, 2, 3 };
                    if (cars == 15) return new int[] { 2, 1, 1, 2, 3, 6, 8 };
                    if (cars == 16) return new int[] { 2, 1, 1, 2, 3, 6, 9 };
                    if (cars >= 17 && cars <= 18) return new int[] { 2, 1, 1, 2, 3, 4, 6 };
                    if (cars == 19) return new int[] { 6, 1, 1, 2, 1, 1, 3, 2, 3, 4, 6, 1, 7, 17, 16, 15, 13, 18, 12 };
                    if (cars == 20) return new int[] { 2, 1, 1, 2, 3, 4, 7 };
                    if (cars >= 21 && cars <= 200) return new int[] { 2, 1, 1, 2, 3, 4, 6 };
                    break;
                case 4:
                    if (cars == 4) return new int[] { 2, 2, 3, 3, 3, 3, 1, 1, 1 };
                    if (cars == 5) return new int[] { 2, 2, 3, 2, 2, 2, 3, 3, 3 };
                    if (cars == 6) return new int[] { 2, 1, 1, 2, 2, 3, 3, 5, 5 };
                    if (cars == 7) return new int[] { 2, 2, 3, 2, 2, 4, 5, 5, 3 };
                    if (cars == 8) return new int[] { 2, 1, 1, 2, 2, 3, 3, 4, 2 };
                    if (cars == 9) return new int[] { 2, 1, 2, 2, 2, 4, 3, 5, 3 };
                    if (cars == 10) return new int[] { 2, 1, 1, 2, 2, 5, 3, 3, 6 };
                    if (cars == 11) return new int[] { 2, 1, 1, 2, 2, 6, 3, 3, 4 };
                    if (cars == 12) return new int[] { 2, 1, 1, 2, 4, 5, 3, 2, 8 };
                    if (cars == 13) return new int[] { 2, 2, 3, 2, 4, 12, 11, 9, 1 };
                    if (cars == 14) return new int[] { 2, 1, 1, 2, 4, 13, 3, 5, 2 };
                    if (cars == 15) return new int[] { 2, 1, 1, 2, 3, 4, 3, 2, 9 };
                    if (cars == 16) return new int[] { 2, 1, 1, 2, 3, 7, 3, 5, 9 };
                    if (cars == 17) return new int[] { 2, 1, 1, 2, 3, 4, 3, 2, 11 };
                    if (cars == 18) return new int[] { 2, 1, 1, 2, 3, 7, 3, 5, 9 };
                    if (cars == 19) return new int[] { 2, 1, 1, 2, 3, 4, 3, 5, 13 };
                    if (cars == 20) return new int[] { 2, 1, 1, 2, 14, 11, 12, 18, 3 };
                    if (cars == 21) return new int[] { 2, 1, 1, 4, 5, 10, 5, 13, 7 };
                    if (cars == 22) return new int[] { 2, 1, 1, 4, 5, 7, 8, 12, 21 };
                    if (cars == 23) return new int[] { 2, 1, 1, 4, 7, 10, 3, 5, 14 };
                    if (cars >= 24 && cars <= 26) return new int[] { 1, 1, 2, 3, 4 };
                    if (cars == 27) return new int[] { 2, 1, 1, 4, 5, 6, 7, 19, 25 };
                    if (cars == 28) return new int[] { 2, 1, 1, 4, 5, 6, 7, 20, 26 };
                    if (cars == 29) return new int[] { 2, 1, 1, 2, 3, 4, 6, 11, 28 };
                    if (cars == 30) return new int[] { 2, 1, 1, 2, 3, 4, 6, 11, 29 };
                    if (cars == 31) return new int[] { 2, 1, 1, 2, 3, 4, 6, 11, 30 };
                    if (cars == 32) return new int[] { 2, 1, 1, 2, 3, 4, 6, 12, 31 };
                    if (cars == 33) return new int[] { 2, 1, 1, 2, 3, 4, 6, 12, 32 };
                    if (cars == 34) return new int[] { 2, 1, 1, 2, 3, 4, 6, 13, 33 };
                    if (cars == 35) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 10 };
                    if (cars == 36) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 12 };
                    if (cars == 37) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 10 };
                    if (cars == 38) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 13 };
                    if (cars >= 39 && cars <= 41) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 10 };
                    if (cars == 42) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11 };
                    if (cars >= 43 && cars <= 47) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 10 };
                    if (cars == 48) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11 };
                    if (cars >= 49 && cars <= 200) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 10 };
                    break;
                case 5:
                    if (cars == 5) return new int[] { 4, 2, 3, 2, 3, 2, 2, 2, 2, 3, 3, 3, 3, 1, 1, 1, 1, 4, 4, 4, 4 };
                    if (cars == 6) return new int[] { 2, 2, 3, 5, 5, 5, 5, 1, 1, 1, 1 };
                    if (cars == 7) return new int[] { 2, 1, 1, 2, 2, 2, 2, 3, 3, 3, 3 };
                    if (cars == 8) return new int[] { 2, 1, 1, 2, 2, 3, 2, 3, 3, 4, 5 };
                    if (cars == 9) return new int[] { 2, 1, 2, 2, 2, 2, 4, 3, 3, 5, 8 };
                    if (cars == 10) return new int[] { 2, 1, 1, 2, 2, 3, 4, 3, 3, 6, 9 };
                    if (cars == 11) return new int[] { 2, 2, 3, 2, 2, 3, 5, 9, 9, 8, 6 };
                    if (cars == 12) return new int[] { 2, 1, 1, 2, 2, 3, 6, 3, 3, 2, 8 };
                    if (cars == 13) return new int[] { 2, 1, 1, 2, 2, 3, 7, 3, 3, 2, 4 };
                    if (cars == 14) return new int[] { 1, 1, 2, 2, 3, 6 };
                    if (cars == 15) return new int[] { 2, 1, 1, 2, 2, 3, 9, 3, 4, 2, 5 };
                    if (cars == 16) return new int[] { 2, 1, 1, 2, 2, 4, 7, 3, 3, 6, 5 };
                    if (cars == 17) return new int[] { 2, 1, 1, 2, 2, 4, 16, 3, 3, 2, 8 };
                    if (cars == 18) return new int[] { 2, 1, 1, 2, 3, 4, 8, 3, 2, 2, 10 };
                    if (cars == 19) return new int[] { 2, 1, 1, 2, 2, 7, 13, 3, 3, 2, 10 };
                    if (cars == 20) return new int[] { 2, 0, 0, 2, 2, 3, 7, 3, 3, 2, 4 };
                    if (cars == 21) return new int[] { 2, 2, 3, 2, 5, 4, 18, 19, 16, 17, 3 };
                    if (cars == 22) return new int[] { 2, 0, 0, 2, 2, 3, 7, 3, 3, 2, 4 };
                    if (cars == 23) return new int[] { 2, 1, 1, 2, 3, 8, 16, 3, 2, 7, 10 };
                    if (cars == 24) return new int[] { 2, 1, 1, 2, 5, 4, 21, 3, 2, 6, 4 };
                    if (cars == 25) return new int[] { 2, 1, 1, 2, 3, 4, 10, 3, 2, 7, 12 };
                    if (cars == 26) return new int[] { 2, 1, 1, 2, 3, 7, 25, 3, 2, 4, 4 };
                    if (cars == 27) return new int[] { 2, 1, 1, 2, 3, 4, 6, 3, 2, 6, 20 };
                    if (cars == 28) return new int[] { 2, 1, 1, 2, 3, 4, 8, 3, 2, 8, 14 };
                    if (cars == 29) return new int[] { 2, 1, 1, 2, 3, 4, 6, 3, 2, 7, 28 };
                    if (cars == 30) return new int[] { 2, 1, 1, 2, 3, 4, 10, 3, 4, 8, 16 };
                    if (cars == 31) return new int[] { 2, 1, 1, 2, 3, 4, 6, 3, 2, 18, 30 };
                    if (cars == 32) return new int[] { 2, 1, 1, 1, 2, 4, 5, 2, 6, 7, 3 };
                    if (cars == 33) return new int[] { 2, 1, 1, 2, 3, 4, 6, 3, 5, 12, 32 };
                    if (cars == 34) return new int[] { 2, 1, 1, 2, 3, 4, 6, 3, 5, 12, 33 };
                    if (cars == 35) return new int[] { 2, 1, 1, 1, 2, 4, 5, 2, 6, 10, 4 };
                    if (cars >= 36 && cars <= 49) return new int[] { 1, 1, 2, 3, 4, 6 };
                    if (cars == 50) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 34 };
                    if (cars == 51) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 18, 34 };
                    if (cars == 52) return new int[] { 1, 1, 2, 3, 4, 6 };
                    if (cars == 53) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 18, 36 };
                    if (cars == 54) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 38 };
                    if (cars == 55) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 12, 17, 54 };
                    if (cars == 56) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 21, 55 };
                    if (cars == 57) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 41 };
                    if (cars == 58) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 42 };
                    if (cars == 59) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 43 };
                    if (cars == 60) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 44 };
                    if (cars >= 61 && cars <= 160) return new int[] { 1, 1, 2, 3, 4, 6 };
                    if (cars >= 161 && cars <= 200) return new int[] { 2, 1, 1, 2, 3, 4, 6, 8, 11, 17, 44 };
                    break;
                case 6:
                    if (cars == 6) return new int[] { 2, 2, 3, 5, 5, 5, 5, 5, 1, 1, 1, 1, 1 };
                    if (cars == 7) return new int[] { 2, 2, 3, 2, 2, 2, 2, 2, 5, 5, 5, 5, 5 };
                    if (cars == 8) return new int[] { 1, 1, 2, 2, 2, 3, 2 };
                    if (cars == 9) return new int[] { 1, 1, 2, 2, 2, 2, 4 };
                    if (cars == 10) return new int[] { 1, 1, 2, 2, 2, 3, 2 };
                    if (cars == 11) return new int[] { 1, 2, 2, 2, 3, 5, 4 };
                    if (cars == 12) return new int[] { 1, 1, 2, 2, 2, 3, 4 };
                    if (cars == 13) return new int[] { 1, 1, 2, 2, 2, 3, 5 };
                    if (cars == 14) return new int[] { 1, 1, 2, 2, 2, 3, 6 };
                    if (cars == 15) return new int[] { 1, 1, 2, 2, 2, 3, 7 };
                    if (cars >= 16 && cars <= 17) return new int[] { 1, 0, 1, 2, 2, 3, 6 };
                    if (cars == 18) return new int[] { 1, 1, 2, 2, 3, 5, 9 };
                    if (cars == 19) return new int[] { 1, 1, 2, 2, 3, 4, 9 };
                    if (cars == 20) return new int[] { 1, 1, 2, 2, 3, 4, 19 };
                    if (cars == 21) return new int[] { 1, 1, 2, 2, 3, 4, 20 };
                    if (cars == 22) return new int[] { 1, 1, 2, 2, 3, 4, 21 };
                    if (cars == 23) return new int[] { 1, 1, 2, 2, 3, 4, 22 };
                    if (cars == 24) return new int[] { 1, 1, 2, 2, 3, 6, 23 };
                    if (cars == 25) return new int[] { 1, 1, 2, 2, 3, 6, 24 };
                    if (cars == 26) return new int[] { 1, 1, 2, 2, 3, 6, 25 };
                    if (cars == 27) return new int[] { 1, 1, 2, 2, 3, 6, 26 };
                    if (cars == 28) return new int[] { 1, 1, 2, 5, 11, 4, 27 };
                    if (cars >= 29 && cars <= 30) return new int[] { 1, 0, 2, 3, 7, 15, 17 };
                    if (cars == 31) return new int[] { 1, 2, 2, 3, 7, 15, 17 };
                    if (cars >= 32 && cars <= 34) return new int[] { 1, 0, 4, 1, 2, 8, 16 };
                    if (cars == 35) return new int[] { 1, 1, 2, 3, 7, 19, 17 };
                    if (cars == 36) return new int[] { 1, 1, 2, 3, 7, 20, 17 };
                    if (cars == 37) return new int[] { 1, 1, 2, 3, 4, 10, 12 };
                    if (cars == 38) return new int[] { 1, 1, 2, 3, 4, 8, 10 };
                    if (cars == 39) return new int[] { 1, 1, 2, 3, 4, 19, 38 };
                    if (cars == 40 || cars == 43 || cars == 45 || cars >= 47 && cars <= 200) return new int[] { 1, 1, 2, 3, 4, 6, 8 };
                    if (cars == 41) return new int[] { 1, 1, 2, 3, 4, 6, 12 };
                    if (cars == 42) return new int[] { 1, 1, 2, 3, 4, 8, 14 };
                    if (cars == 44 || cars == 46) return new int[] { 1, 1, 2, 3, 4, 6, 11 };
                    break;
            }
            throw new Exception("Inappropriate Parameters in int[] EnhancedLaneRotation.getRuns(int l, int c)");
        }
        #endregion int[] getRuns(int l, int c)

        #region List<int[]> cleanupRuns(int[] run, int lanes, int runs)
        private List<int[]> CleanupRuns(int[] run, int lanes, int runs)
        {
            if (runs < 1 || runs > run[0])
                throw new Exception("Inappropriate Parameters in int[] EnhancedLaneRotation.cleanupRuns(int[] run, int lanes, int runs) { runs }");
            if (lanes < 2 || lanes > 6)
                throw new Exception("Inappropriate Parameters in int[] EnhancedLaneRotation.cleanupRuns(int[] run, int lanes, int runs) { lanes }");
            if (run.Length != 1 + (run[0] * lanes))
                throw new Exception("Inappropriate Parameters in int[] EnhancedLaneRotation.cleanupRuns(int[] run, int lanes, int runs) { run }");

            // We limit ourselves to only the runs desired
            var v = run.Skip(runs + 1).ToArray();
            v = v.Take(runs * (lanes - 1)).ToArray();

            // Take each, and add a "0" as the first element, which makes later operations easier
            List<int[]> RunSet = new List<int[]>();
            while (v.Count() > 0)
            {
                RunSet.Add(new int[] { 0 }.Concat(v.Take(lanes - 1)).ToArray());
                v = v.Skip(lanes - 1).ToArray();
            }

            // The lists we have are by refrence, so "3" means 3 spaces after the last place.
            // We are going to convert that to an absolute # instead, to make later actions easier.
            List<int[]> NewRunSet = new List<int[]>();
            foreach (int[] x in RunSet)
            {
                int[] newX = x;
                int sum = 0;
                for (int i = 1; i < x.Length; i++)
                {
                    sum += x[i];
                    newX[i] = sum;
                }
                NewRunSet.Add(newX);
            }

            return NewRunSet;
        }
        #endregion List<int[]> cleanupRuns(int[] run, int lanes, int runs)

    }
}
