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
        public void Cancel()
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

        public Derby GetCurrentDerby()
        {
            throw new NotImplementedException();
        }

        public Derby GetCurrentDerbyWithDivisions()
        {
            throw new NotImplementedException();
        }

        public List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
