using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EF
{
    public partial class ContosoUniversityEntities : DbContext
    {
        public override int SaveChanges()
        {
            var Entities = this.ChangeTracker.Entries();

            foreach (var entity in Entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        break;
                    case EntityState.Deleted:
                        break;
                    case EntityState.Detached:
                        break;
                    case EntityState.Modified:
                        break;
                    case EntityState.Unchanged:
                        break;
                    default:
                        break;
                }
                {
 
                }
                if (entity.Entity is Course)
                {
                    entity.CurrentValues.SetValues(new
                    {
                        ModifiedOn = DateTime.Now
                    });
                }
            }
            return base.SaveChanges();
        }
    }
}
