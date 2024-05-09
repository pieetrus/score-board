using ScoreBoard.Application.Interfaces;
using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Domain.Models;

namespace ScoreBoard.Application.Services
{
    public class ScoreboardService(IMatchRepository matchRepository) : IScoreboardService
    {
        public void StartMatch(string homeTeam, string awayTeam)
        {
            matchRepository.Add(Match.Create(homeTeam, awayTeam));
        }

        public void UpdateScores(string v1, string v2, int v3, int v4)
        {
            throw new NotImplementedException();
        }
    }
}