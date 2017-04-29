using System;

namespace DerbyManagement.BLL
{
    internal static class Utilities
    {
        public static void CheckScheduleInputs(int lanes, int cars, int? runs = null)
        {
            if (lanes < 2) throw new ArgumentOutOfRangeException("Must have at least 2 lanes.");
            if (lanes > 6) throw new ArgumentOutOfRangeException("May have at most 6 lanes.");
            if (cars < 2) throw new ArgumentOutOfRangeException("Must have at least 2 cars.");
            if (cars > 200) throw new ArgumentOutOfRangeException("The maximum number of cars is 200.");
            if (cars < lanes) throw new ArgumentOutOfRangeException("Must have at least as many cars as lanes.");
            if (runs != null)
            {
                if (runs < 1) throw new ArgumentOutOfRangeException("Must have at least 1 run.");
                if (runs > 12) throw new ArgumentOutOfRangeException("Maximum nuber of runs per lane is 12.");
            }
        }
    }
}
