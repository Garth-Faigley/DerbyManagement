namespace DerbyManagement.BLL
{
    public class SchedulerDetails
    {
        public string ClassName { get; private set; }
        public string SchedulerName { get; private set; }
        public string Description { get; private set; }

        public SchedulerDetails(string ClassName, string SchedulerName, string Description)
        {
            this.ClassName = ClassName;
            this.SchedulerName = SchedulerName;
            this.Description = Description;
        }
    }
}