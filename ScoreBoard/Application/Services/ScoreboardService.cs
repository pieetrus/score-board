using ScoreBoard.Application.Exceptions;
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

        public void UpdateScores(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore)
        {
            var match = matchRepository.GetByTeamNames(homeTeamName, awayTeamName);

            if (match is null)
            {
                throw new MatchNotFoundException(homeTeamName, awayTeamName);
            }

            match.UpdateScores(homeTeamScore, awayTeamScore);
            matchRepository.Update(match);
        }
    }
}