﻿using DerbyManagement.Model;
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
        int CheckCarNumberUnique(int derbyId, int racerId, int carNumber);

        void Save();
        void Cancel();
    }
}
