using DerbyManagement.Model;
using Moq;
using System.Collections.Generic;

namespace DerbyManagement.Tests.BllTests
{
    public class MockSchedulingData
    {
        public Division _division;
        public List<Racer> _racers;

        public MockSchedulingData()
        {
            _division = new Division() { DivisionId = 1, Name = "Division 1", IncludeInChampionship = true };
            
            // Create 25 mock racers
            _racers = new List<Racer>();

            for (int i = 1; i <= 25; i++)
            {
                var mockRacer = Mock.Of<Racer>(r => r.CarNumber == (i + 10) && r.RacerId == i);
                mockRacer.Divisions.Add(_division);
                _racers.Add(mockRacer);
            }

        }

    }
}
