using Microsoft.EntityFrameworkCore;
using Sydy.Gambling.Football.Data.Models;

namespace Sydy.Gambling.Football.Web.API.Tests.Extensions
{
    public static class DbSetExtensions
    {
        public static DbSet<Team> Seed(this DbSet<Team> teams)
        {
            var seeds = new[]
            {
                new Team() { Name = "Luverdense" },
                new Team() { Name = "Jangada FC" },
                new Team() { Name = "Cuiabá" },
                new Team() { Name = "Corinthians" },
                new Team() { Name = "Flamengo" },
                new Team() { Name = "Cruzeiro" },
                new Team() { Name = "São Paulo" },
            };

            teams.AddRange(seeds);

            return teams;
        }
    }
}
