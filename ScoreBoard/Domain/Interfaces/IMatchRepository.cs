using ScoreBoard.Domain.Models;

namespace ScoreBoard.Domain.Interfaces
{
    public interface IMatchRepository
    {
        void Add(Match match);
        IEnumerable<Match> GetAll();
        Match? GetByTeamNames(string teamHomeName, string teamAwayName);
        void Remove(Match match);
        void Update(Match match);
    }
}
