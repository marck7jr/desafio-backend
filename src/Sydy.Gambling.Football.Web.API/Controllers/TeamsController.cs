using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using Sydy.Gambling.Football.Services;
using Sydy.Gambling.Football.Web.API.Infrastructure;
using System.Linq;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API.Controllers
{
    [Route("api/time")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Time")]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMatchService _matchService;
        private readonly ITeamsService _teamsService;

        public TeamsController(ApplicationDbContext context, IMatchService matchService, ITeamsService teamsService)
        {
            _applicationDbContext = context;
            _matchService = matchService;
            _teamsService = teamsService;
        }

        // GET: api/time
        [HttpGet]
        public async Task<ActionResult<GetTeamsResponse>> GetTeams([FromQuery] int pagina = 1, [FromQuery] int tamanhoPagina = 10)
        {
            double teamsCount = await _applicationDbContext.Teams.AsAsyncQueryable().CountAsync();

            var teams = await _teamsService.GetTeamsAsync(pagina, tamanhoPagina)
                .Cast<Team>()
                .ToListAsync();

            if (teams.Any())
            {
                GetTeamsResponse response = new(pagina, teamsCount, tamanhoPagina, teams);

                return response;
            }

            return NotFound();
        }

        // GET: api/time/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam([FromRoute] int id)
        {
            var team = await _applicationDbContext.Teams.FindAsync(id);

            if (team is null)
            {
                return NotFound();
            }

            return team;
        }


        // GET: api/time/5/estatisticas
        [HttpGet("{id}/estatisticas")]
        public async Task<ActionResult<GetTeamStatsResponse>> GetTeamStats([FromRoute] int id)
        {
            var team = await _applicationDbContext.Teams.FindAsync(id);

            if (team is null)
            {
                return NotFound();
            }

            var matches = await _matchService.GetMatchesAsync(team).ToListAsync();

            if (matches is { Count: >= 1 })
            {
                GetTeamStatsResponse response = new(team, matches);

                return response;
            }

            return NoContent();
        }

        // PUT: api/time/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam([FromRoute] int id, [FromBody] Team team)
        {
            if (TeamExists(id))
            {
                team.Id = id;

                _applicationDbContext.Entry(team).State = EntityState.Modified;

                try
                {
                    await _applicationDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return NoContent();
            }

            ModelState.AddModelError(nameof(Team.Id), $"Não existe registro com a chave-primária fornecida.");

            return BadRequest(ModelState);
        }

        // POST: api/time
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam([FromBody] Team team)
        {
            if (ModelState.IsValid)
            {
                if (await _applicationDbContext.Teams.AsAsyncQueryable().AnyAsync(x => x.Name == team.Name))
                {
                    ModelState.AddModelError(nameof(Team.Name), $"Já existe um time com este nome.");

                    return Conflict(ModelState);
                }

                _applicationDbContext.Teams.Add(team);
                await _applicationDbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetTeam), new { id = team.Id }, team);
            }

            return BadRequest(ModelState.Values);
        }

        // DELETE: api/time/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _applicationDbContext.Teams.FindAsync(id);

            if (team is null)
            {
                return NotFound();
            }

            _applicationDbContext.Teams.Remove(team);
            await _applicationDbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool TeamExists(int id)
        {
            return _applicationDbContext.Teams.Any(e => e.Id == id);
        }
    }
}
