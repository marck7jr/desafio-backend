using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Sydy.Gambling.Football.Data.DesignTime
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new()
            {
                DataSource = ".",
                InitialCatalog = "Football",
                UserID = "SA",
                Password = "22e18087-93cd-4f23-8edb-7fdc45b5fd55",
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionStringBuilder.ConnectionString);

            return new(optionsBuilder.Options);
        }
    }
}
