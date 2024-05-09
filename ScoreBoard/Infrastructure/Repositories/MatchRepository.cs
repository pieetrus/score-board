using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Domain.Models;
using ScoreBoard.Infrastructure.Exceptions;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ScoreBoard.Tests")]
namespace ScoreBoard.Infrastructure.Repositories
{
    internal sealed class MatchRepository : IMatchRepository
    {
        private readonly List<Match> _matches = new();

        public void Add(Match match)
        {
            if (GetByTeamNames(match.HomeTeam, match.AwayTeam) is not null)
            {
                throw new MatchBeetweenTheseTeamsAlreadyExistsException(match.HomeTeam, match.AwayTeam);
            }

            _matches.Add(match);
        }

        public IEnumerable<Match> GetAll() => _matches;

        public Match? GetByTeamNames(string teamHomeName, string teamAwayName)
            => _matches.FirstOrDefault(m => m.HomeTeam == teamHomeName && m.AwayTeam == teamAwayName);

        public void Remove(Match match)
        {
            var existingMatch = GetByTeamNames(match.HomeTeam, match.AwayTeam);

            if (existingMatch is null)
            {
                throw new MatchDoesNotExistsException(match.HomeTeam, match.AwayTeam);
            }

            _matches.Remove(existingMatch);
        }

        public void Update(Match match)
        {
            var existingMatch = GetByTeamNames(match.HomeTeam, match.AwayTeam);
            if (existingMatch is not null)
            {
                existingMatch.UpdateScores(match.HomeScore, match.AwayScore);
            }
        }
    }
}