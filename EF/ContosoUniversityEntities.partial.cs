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

                    case EntityState.Unchanged:
                    default:
                        break;
                    case EntityState.Deleted:

                    case EntityState.Detached:

                    case EntityState.Modified:
                        entity.CurrentValues.SetValues(new
                        {
                            ModifiedOn = DateTime.Now
                        });
                        break;

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
