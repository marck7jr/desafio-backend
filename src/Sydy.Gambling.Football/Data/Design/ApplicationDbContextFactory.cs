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
                Password = "468b53d5-e3e8-41db-af46-d9f7977b981f",
            };

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer(connectionStringBuilder.ConnectionString);

            return new(optionsBuilder.Options);
        }
    }
}
