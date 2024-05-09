using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Domain.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ScoreBoard.Tests")]
namespace ScoreBoard.Infrastructure.Repositories
{
    internal sealed class MatchRepository : IMatchRepository
    {
        public void Add(Match match)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Match> GetAll()
        {
            throw new NotImplementedException();
        }

        public Match? GetByTeamNames(string teamHomeName, string teamAwayName)
        {
            throw new NotImplementedException();
        }

        public void Remove(Match match)
        {
            throw new NotImplementedException();
        }

        public void Update(Match match)
        {
            throw new NotImplementedException();
        }
    }
}
