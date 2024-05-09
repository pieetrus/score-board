using ScoreBoard.Domain.Models;

namespace ScoreBoard.Application.Interfaces
{
    public interface IScoreboardService
    {
        void FinishMatch(string homeTeamName, string awayTeamName);
        IEnumerable<Match> GetSummary();
        void StartMatch(string homeTeamName, string awayTeamName);
        void UpdateScores(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore);
    }
}