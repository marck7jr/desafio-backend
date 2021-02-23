using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Services;
using Sydy.Gambling.Football.Tests.Extensions;
using System;
using System.Diagnostics;

namespace Sydy.Gambling.Football.Tests
{
    [TestClass]
    public class Program
    {
        public static IServiceProvider Services { get; private set; }

        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext _)
        {
            var builder = Host.CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddInMemoryDbContext<ApplicationDbContext>();
                    services.AddTransient<IMatchService, MatchService>();
                    services.AddTransient<ITeamsService, TeamsService>();
                    services.AddTransient<ITournamentService, TournamentService>();

                    using var scope = services.BuildServiceProvider().CreateScope();
                    var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                    try
                    {
                        applicationDbContext.Teams.Seed();
                        applicationDbContext.SaveChanges();
                    }
                    catch (Exception exception)
                    {
                        Debug.WriteLine(exception);
                    }
                });

            var host = builder.Build();

            Services = host.Services;
        }
    }
}
