using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Sydy.Gambling.Football.Tests.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryDbContext<T>(this IServiceCollection services)
            where T : DbContext
        {
            var serviceDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<T>));

            services.Remove(serviceDescriptor);

            services.AddDbContext<T>(options =>
            {
                options.UseInMemoryDatabase(typeof(T).FullName);
            });

            return services;
        }
    }
}
