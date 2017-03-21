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
        Division CreateDivision();
        int CheckSequenceNumberUnique(int derbyId, int divisionId, int sequenceNumber);

        // Racer
        List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId);
        Racer CreateRacer();
        void DeleteRacer(Racer racer);
        int CheckCarNumberUnique(int derbyId, int racerId, int carNumber);

        void Save();
        void Cancel();
    }
}
