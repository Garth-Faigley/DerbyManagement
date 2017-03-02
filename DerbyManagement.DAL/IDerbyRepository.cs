using DerbyManagement.Model;
using System.Collections.Generic;

namespace DerbyManagement.DAL
{
    public interface IDerbyRepository
    {
        // Derby
        Derby GetCurrentDerbyWithDivisions();

        // Racer
        List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId);  
    }
}
