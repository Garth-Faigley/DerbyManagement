using DerbyManagement.Model;
using DerbyManagement.DAL;
using System.Collections.Generic;

namespace DerbyManagement.App.Services
{
    public class DerbyDataService : IDerbyDataService
    {
        IDerbyRepository _repository;

        public DerbyDataService(IDerbyRepository repository)
        {
            _repository = repository;
        }

        #region " Derby "
        public Derby GetCurrentDerby()
        {
            return _repository.GetCurrentDerby();
        }

        public Derby GetCurrentDerbyWithDivisions()
        {
            return _repository.GetCurrentDerbyWithDivisions();
        }
        #endregion

        public List<Division> GatAllDivisionsExceptChampionship(int derbyId)
        {
            return _repository.GatAllDivisionsExceptChampionship(derbyId);
        }

        #region " Racers "

        public List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId)
        {
            return _repository.GetRacersByDerbyIdWithDivisions(derbyId);
        }

        public Racer CreateRacer()
        {
            return _repository.CreateRacer();
        }

        public void DeleteRacer(Racer racer)
        {
            _repository.DeleteRacer(racer);
        }

        #endregion

        public void Save()
        {
            _repository.Save();
        }

        public void Cancel()
        {
            _repository.Cancel();
        }

    }
}
