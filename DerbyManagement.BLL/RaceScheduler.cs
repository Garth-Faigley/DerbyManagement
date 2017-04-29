using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

// Downloaded from:  https://github.com/RyanJean/DerbyTime/tree/master/DerbyRaceScheduler

namespace DerbyManagement.BLL
{
    public static class RaceScheduler
    {
        public static SchedulerDetails[] GetAllSchedulers()
        {
            var schedulers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == "DerbyManagement.BLL.Schedulers");
            schedulers = schedulers.Where(t => !Regex.IsMatch(t.Name, "[<>+_]")).OrderBy(t => t.Name);

            var details = schedulers.Select(a => (IScheduler)Activator.CreateInstance(a)).Select(d => d.Details);
            return details.ToArray();
        }

        public static IScheduler GetScheduler(SchedulerDetails details)
        {
            var scheduler = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsClass && t.Namespace == "DerbyManagement.BLL.Schedulers" && t.Name == details.ClassName).FirstOrDefault();
            return (IScheduler)Activator.CreateInstance(scheduler);
        }
    }
}
