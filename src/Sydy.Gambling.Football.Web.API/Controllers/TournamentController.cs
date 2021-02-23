using Microsoft.AspNetCore.Mvc;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using Sydy.Gambling.Football.Services;
using Sydy.Gambling.Football.Web.API.Infrastructure;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API.Controllers
{
    [Route("api/campeonato")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private ApplicationDbContext _applicationDbContext;
        private ITournamentService _tournamentService;

        public TournamentController(ApplicationDbContext applicationDbContext, ITournamentService tournamentService)
        {
            _applicationDbContext = applicationDbContext;
            _tournamentService = tournamentService;
        }

        [HttpGet]
        public async Task<GetTournamentResponse> GetTournament()
        {
            var tournament = (Tournament)await _tournamentService.GetTournamentAsync();

            _applicationDbContext.Tournaments.Add(tournament);
            await _applicationDbContext.SaveChangesAsync();

            GetTournamentResponse response = new(tournament);

            return response;
        }
    }
}
