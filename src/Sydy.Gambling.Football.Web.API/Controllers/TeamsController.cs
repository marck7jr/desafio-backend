using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using Sydy.Gambling.Football.Services;
using Sydy.Gambling.Football.Web.API.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sydy.Gambling.Football.Web.API.Controllers
{
    [Route("api/time")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ITeamsService _teamsService;

        public TeamsController(ApplicationDbContext context, ITeamsService teamsService)
        {
            _applicationDbContext = context;
            _teamsService = teamsService;
        }

        // GET: api/Teams
        [HttpGet]
        public async Task<ActionResult<GetTeamsResponse>> GetTeams(int pagina = 1, int tamanhoPagina = 10)
        {
            double teamsCount = await _applicationDbContext.Teams.AsAsyncQueryable().CountAsync();

            double pagesCount = teamsCount / tamanhoPagina;

            var teams = await _teamsService.GetTeamsAsync(pagina, tamanhoPagina)
                .Cast<Team>()
                .ToListAsync();

            GetTeamsResponse response = new()
            {
                Count = (int)Math.Round(pagesCount, MidpointRounding.AwayFromZero),
                Page = pagina,
                Size = tamanhoPagina,
                Teams = teams,
            };

            return response;
        }

        // GET: api/Teams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Team>> GetTeam(int id)
        {
            var team = await _applicationDbContext.Teams.FindAsync(id);

            if (team is null)
            {
                return NotFound();
            }

            return team;
        }

        // PUT: api/Teams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeam(int id, Team team)
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

        // POST: api/Teams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Team>> PostTeam(Team team)
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

        // DELETE: api/Teams/5
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
