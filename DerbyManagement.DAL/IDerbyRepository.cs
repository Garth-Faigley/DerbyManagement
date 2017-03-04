using DerbyManagement.Model;
using System.Collections.Generic;

namespace DerbyManagement.DAL
{
    public interface IDerbyRepository
    {
        // Derby
        Derby GetCurrentDerby();
        Derby GetCurrentDerbyWithDivisions();

        // Racer
        List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId);

        void Save();
        void Cancel();
    }
}
