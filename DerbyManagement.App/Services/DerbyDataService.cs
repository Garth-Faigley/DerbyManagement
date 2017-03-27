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

        #region " Division "
        public List<Division> GetAllDivisionsExceptChampionship(int derbyId)
        {
            return _repository.GatAllDivisionsExceptChampionship(derbyId);
        }

        public Division CreateDivision()
        {
            return _repository.CreateDivision();
        }

        public void DeleteDivision(Division division)
        {
            _repository.DeleteDivision(division);
        }

        public int CheckSequenceNumberUnique(int derbyId, int divisionId, int sequenceNumber)
        {
            return _repository.CheckSequenceNumberUnique(derbyId, divisionId, sequenceNumber);
        }
        #endregion

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

        public int CheckCarNumberUnique(int derbyId, int racerId, int carNumber)
        {
            return _repository.CheckCarNumberUnique(derbyId, racerId, carNumber);
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
