using DerbyManagement.Model;
using System.Collections.Generic;

namespace DerbyManagement.App.Services
{
    public interface IDerbyDataService
    {
        // Derby
        Derby GetCurrentDerby();
        Derby GetCurrentDerbyWithDivisions();

        // Racer
        List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId);
        Racer CreateRacer();

        void Save();
        void Cancel();
    }
}
