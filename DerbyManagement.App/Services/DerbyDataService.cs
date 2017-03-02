using DerbyManagement.Model;
using DerbyManagement.DAL;
using System;
using System.Collections.Generic;

namespace DerbyManagement.App.Services
{
    class DerbyDataService : IDerbyDataService
    {
        IDerbyRepository _repository;

        public DerbyDataService(IDerbyRepository repository)
        {
            _repository = repository;
        }

        public Derby GetCurrentDerbyWithDivisions()
        {
            return _repository.GetCurrentDerbyWithDivisions();
        }

        public List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId)
        {
            return _repository.GetRacersByDerbyIdWithDivisions(derbyId);
        }
    }
}
