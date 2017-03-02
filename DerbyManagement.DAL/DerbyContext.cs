using DerbyManagement.Model;
using DerbyManagement.Model.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;

namespace DerbyManagement.DAL
{
    public class DerbyContext : DbContext
    {
        public DbSet<Derby> Derbies { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Heat> Heats { get; set; }
        public DbSet<Run> Runs { get; set; }
        public DbSet<Racer> Racers { get; set; }

        // Don't persist any 'IsDirty' flags in the database.  They are cient-side only.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Types()
                .Configure(c => c.Ignore("IsDirty"));
            base.OnModelCreating(modelBuilder);
        }

        // Automatically handle DateCreated and DateModified.  Also reset
        // IsDirty flag to false after successful save.
        public override int SaveChanges()
        {
            foreach (var history in this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModificationHistory && (e.State == EntityState.Added ||
                            e.State == EntityState.Modified))
                .Select(e => e.Entity as IModificationHistory)
                )
            {
                history.DateModified = DateTime.Now;
                if (history.DateCreated == DateTime.MinValue)
                {
                    history.DateCreated = DateTime.Now;
                }
            }
            int result = base.SaveChanges();
            foreach (var history in this.ChangeTracker.Entries()
                .Where(e => e.Entity is IModificationHistory) 
                .Select(e => e.Entity as IModificationHistory)
                )
            {
                history.IsDirty = false;
            }
            return result;
        }

    }
}
