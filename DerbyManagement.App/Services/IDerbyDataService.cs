using DerbyManagement.Model;

namespace DerbyManagement.App.Services
{
    public interface IDerbyDataService
    {
        Derby GetCurrentDerbyWithDivisions();
    }
}
