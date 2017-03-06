using DerbyManagement.Model;
using System.Collections.Generic;

namespace DerbyManagement.DAL
{
    public interface IDerbyRepository
    {
        // Derby
        Derby GetCurrentDerby();
        Derby GetCurrentDerbyWithDivisions();

        // Division
        List<Division> GatAllDivisionsExceptChampionship(int derbyId);

        // Racer
        List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId);
        Racer CreateRacer();
        void DeleteRacer(Racer racer);

        void Save();
        void Cancel();
    }
}
