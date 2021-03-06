﻿using Microsoft.EntityFrameworkCore;
using Sydy.Gambling.Football.Data;
using Sydy.Gambling.Football.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Sydy.Gambling.Football.Services
{
    public class MatchService : IMatchService
    {
        private const int MaxValue = 5;

        private readonly ApplicationDbContext _applicationDbContext;

        private readonly Random random = new(2021);

        public MatchService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async IAsyncEnumerable<IMatch> GetMatchesAsync(ITeam team, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var matches = _applicationDbContext.Matches
                .Include(matches => matches.Results)
                .ThenInclude(results => results.Team)
                .Where(matches => matches.Results!.Any(results => results.Team!.Id == team.Id))
                .AsAsyncEnumerable();

            await foreach (var match in matches)
            {
                cancellationToken.ThrowIfCancellationRequested();

                yield return match;
            }
        }

        public async IAsyncEnumerable<IMatch> GetMatchesAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var teams = await _applicationDbContext.Teams.AsAsyncQueryable().ToListAsync(cancellationToken);

            if (teams.Count < 4)
            {
                yield break;
            }

            int k = 1;

            for (int i = 0; i < teams.Count; i++)
            {
                for (int j = k; j < teams.Count; j++)
                {
                    cancellationToken.ThrowIfCancellationRequested();

                    Match match = new()
                    {
                        Results = new List<MatchResult>
                        {
                            new(){ Team = teams.ElementAt(i), Score = random.Next(MaxValue) },
                            new(){ Team = teams.ElementAt(j), Score = random.Next(MaxValue) }
                        },
                    };

                    yield return match;
                }

                k++;
            }
        }
    }
}
