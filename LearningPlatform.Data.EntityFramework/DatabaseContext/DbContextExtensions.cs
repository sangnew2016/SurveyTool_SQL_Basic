using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace LearningPlatform.Data.EntityFramework.DatabaseContext
{
    public static class DbContextExtensions
    {
        public static bool IsDirty(this DbContext context)
        {            
            // Query the change tracker entries for any adds, modifications, or deletes.
            IEnumerable<DbEntityEntry> res = from e in context.ChangeTracker.Entries()
                where e.State.HasFlag(EntityState.Added) ||
                      e.State.HasFlag(EntityState.Modified) ||
                      e.State.HasFlag(EntityState.Deleted)
                select e;

            if (res.Any())
                return true;

            return false;

        }
    }
}
