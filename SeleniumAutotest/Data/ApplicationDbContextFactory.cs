using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SeleniumAutotest.Data
{
    public class ApplicationDbContextFactory //: IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        IConfiguration configuration;

        public ApplicationDbContextFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"]);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
