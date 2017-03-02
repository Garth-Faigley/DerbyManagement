using DerbyManagement.Model;
using System.Collections.Generic;

namespace DerbyManagement.App.Services
{
    public interface IDerbyDataService
    {
        // Derby
        Derby GetCurrentDerbyWithDivisions();

        // Racer
        List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId);
    }
}
