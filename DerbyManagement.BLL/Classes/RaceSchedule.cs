namespace DerbyManagement.BLL
{
    public class RaceSchedule
    {
        public string Name { get; private set; }
        public int Lanes { get; private set; }
        public int Cars { get; private set; }
        public int Runs { get; private set; }
        public int[][] Heats { get; private set; }

        public RaceSchedule(string Name, int Lanes, int Cars, int Runs, int[][] Heats)
        {
            this.Name = Name;
            this.Lanes = Lanes;
            this.Cars = Cars;
            this.Runs = Runs;
            this.Heats = Heats;
        }
    }
}