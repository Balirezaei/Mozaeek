using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MozaeekCore.TechnicianProfileConsistensyService.Context
{
    public class TechnicianProfileContextFactory : IDesignTimeDbContextFactory<TechnicianProfileContext>
    {
        public TechnicianProfileContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TechnicianProfileContext>();

            optionsBuilder.UseSqlServer("integrated Security=True;Initial Catalog=MozaeekTechnicianProfileDevelopment;Data Source=.");

            return new TechnicianProfileContext(optionsBuilder.Options);
        }
    }
}