using ScoreBoard.Application.DTOs;
using ScoreBoard.Domain.Models;

namespace ScoreBoard.Application
{
    internal static class Extensions
    {
        public static MatchDto AsDto(this Match readModel)
            => new()
            {
                HomeTeam = readModel.HomeTeam,
                AwayTeam = readModel.AwayTeam,
                HomeScore = readModel.HomeScore,
                AwayScore = readModel.AwayScore
            };
    }
}
