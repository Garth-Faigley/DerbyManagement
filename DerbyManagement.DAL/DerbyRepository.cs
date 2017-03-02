using DerbyManagement.Model;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

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

        public List<Racer> GetRacersByDerbyIdWithDivisions(int derbyId)
        {
            return _context.Racers.Include(r => r.Divisions)
                .Where(r => r.Divisions.Any(d => d.DerbyId == derbyId))
                .ToList();
        }
    }
}
