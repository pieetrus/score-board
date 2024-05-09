using ScoreBoard.Domain.Models;

namespace ScoreBoard.Domain.Interfaces
{
    public interface IMatchRepository
    {
        IEnumerable<Match> GetAll();
        Match? GetByTeamNames(string teamHomeName, string teamAwayName);
        void Add(Match match);
        void Update(Match match);
        void Remove(Match match);
    }
}
