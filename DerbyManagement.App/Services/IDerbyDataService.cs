﻿using DerbyManagement.Model;
using System.Collections.Generic;

namespace DerbyManagement.App.Services
{
    public interface IDerbyDataService
    {
        // Derby
        Derby GetCurrentDerby();
        Derby GetCurrentDerbyWithDivisions();

        // Division
        List<Division> GetAllDivisionsExceptChampionship(int derbyId);
        Division CreateDivision();
        void DeleteDivision(Division division);
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
