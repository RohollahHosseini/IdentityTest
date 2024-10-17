using Microsoft.EntityFrameworkCore;

namespace IdentityTests.EFCore.Context
{
    public class IdentityTestDbContext(DbContextOptions<IdentityTestDbContext> options):DbContext(options)
    {


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityTestDbContext).Assembly) ;

            base.OnModelCreating(modelBuilder); 
        }

    }
}
