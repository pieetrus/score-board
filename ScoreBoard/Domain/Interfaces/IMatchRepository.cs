using ScoreBoard.Domain.Models;

namespace ScoreBoard.Domain.Interfaces
{
    public interface IMatchRepository
    {
        void Add(Match match);
        Match? GetByTeamNames(string teamHomeName, string teamAwayName);
        void Update(Match match);
    }
}
