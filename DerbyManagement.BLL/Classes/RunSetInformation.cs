namespace DerbyManagement.BLL
{
    public class RunSetInformation
    {
        public int NumberOfRuns { get; private set; }
        public string RunSetName { get; private set; }

        public RunSetInformation(int NumberOfRuns, string RunSetName)
        {
            this.NumberOfRuns = NumberOfRuns;
            this.RunSetName = RunSetName;
        }
    }
}