using DerbyManagement.Model;
using System.Data.Entity;
using System.Linq;

namespace DerbyManagement.DAL
{
    public class DerbyRepository : IDerbyRepository
    {
        readonly DerbyContext _context = new DerbyContext();

        public Derby GetCurrentDerbyWithDivisions()
        {
            return _context.Derbies.Include(d => d.Divisions)
                .OrderByDescending(d => d.DerbyId)
                .FirstOrDefault();
        }
    }
}
