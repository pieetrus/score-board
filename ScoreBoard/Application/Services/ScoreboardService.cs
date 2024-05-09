using ScoreBoard.Application.DTOs;
using ScoreBoard.Application.Exceptions;
using ScoreBoard.Application.Interfaces;
using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Domain.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("ScoreBoard.Tests")]
namespace ScoreBoard.Application.Services
{
    internal sealed class ScoreboardService(IMatchRepository matchRepository) : IScoreboardService
    {
        public void FinishMatch(string homeTeamName, string awayTeamName)
        {
            var match = matchRepository.GetMatchByTeamNames(homeTeamName, awayTeamName);

            if (match is null)
            {
                throw new MatchNotFoundException(homeTeamName, awayTeamName);
            }

            matchRepository.Remove(match);
        }

        public IEnumerable<MatchDto> GetSummary()
        {
            var matches = matchRepository.GetAll();

            var result = matches.OrderByDescending(m => m.HomeScore + m.AwayScore)
                .ThenByDescending(m => m.StartTime) // Sort by most recently started
                .Select(m => m.AsDto());

            return result;
        }

        public void StartMatch(string homeTeamName, string awayTeamName)
        {
            matchRepository.Add(Match.Create(homeTeamName, awayTeamName));
        }

        public void UpdateScores(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore)
        {
            var match = matchRepository.GetMatchByTeamNames(homeTeamName, awayTeamName);

            if (match is null)
            {
                throw new MatchNotFoundException(homeTeamName, awayTeamName);
            }

            match.UpdateScores(homeTeamScore, awayTeamScore);
            matchRepository.Update(match);
        }
    }
}