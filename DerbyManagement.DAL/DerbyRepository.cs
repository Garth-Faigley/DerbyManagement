using DerbyManagement.Model;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace DerbyManagement.DAL
{
    public class DerbyRepository : IDerbyRepository
    {
        readonly DerbyContext _context = new DerbyContext();

        public Derby GetCurrentDerby()
        {
            return _context.Derbies
                .OrderByDescending(d => d.DerbyId)
                .FirstOrDefault();
        }

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

        public void Save()
        {
            RemoveEmptyNewObjects();
            _context.SaveChanges();
        }

        public void Cancel()
        {
            foreach (DbEntityEntry entry in _context.ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged; break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached; break;
                    case EntityState.Deleted:
                        entry.Reload(); break;
                    default: break;
                }
            }
        }

        private void RemoveEmptyNewObjects()
        {
            //you can't remove from or add to a collection in a foreach loop

            // Racers
            for (var i = _context.Racers.Local.Count; i > 0; i--)
            {
                var racer = _context.Racers.Local[i - 1];
                if (_context.Entry(racer).State == EntityState.Added
                    && !racer.IsDirty)
                {
                    _context.Racers.Remove(racer);
                }
            }
        }
  
    }
}
