using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DerbyManagement.BLL
{
    public interface IScheduler
    {

        SchedulerDetails Details { get; }
        RunSetInformation[] GetAvailableRuns(int lanes, int cars);
        RaceSchedule GetRaceSchedule(int lanes, int cars, int runs, bool shuffle = false);

        /*
        // Default format for scheduler details section
        SchedulerDetails IScheduler.Details { get { return new SchedulerDetails(
            this.GetType().Name,
            "SCHEDULER'S COMMON NAME",
            "SCHEDULER'S DESCRIPTION"
        ); } }
        */

        /*
        // First line of both methods
        Utilities.ExceptionCheck(lanes, cars);
        */


    }
}
