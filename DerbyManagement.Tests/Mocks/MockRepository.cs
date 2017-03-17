using DerbyManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DerbyManagement.Model;

namespace DerbyManagement.Tests.Mocks
{
    class MockRepository : IDerbyRepository
    {

        #region " Derby "
        public Derby GetCurrentDerby()
        {
            throw new NotImplementedException();
        }

        public Derby GetCurrentDerbyWithDivisions()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region " Division " 
        public List<Division> GatAllDivisionsExceptChampionship(int derbyId)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region " Racer " 
        public List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId)
        {
            throw new NotImplementedException();
        }

        public Racer CreateRacer()
        {
            throw new NotImplementedException();
        }

        public void DeleteRacer(Racer racer)
        {
            throw new NotImplementedException();
        }

        public int CheckCarNumberUnique(int derbyId, int racerId, int carNumber)
        {
            throw new NotImplementedException();
        }
        #endregion

        public void Cancel()
        {
            // Don't need to do anything in test.
        }

        public void Save()
        {
            // Don't need to do anything in test.
        }

    }
}
