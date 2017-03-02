using DerbyManagement.Model;
using DerbyManagement.DAL;

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
    }
}
