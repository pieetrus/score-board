using ScoreBoard.Shared.Exceptions;

namespace ScoreBoard.Infrastructure.Exceptions
{
    public class MatchDoesNotExistsException(string homeTeamName, string awayTeamName)
        : ScoreBoardException($"Match with homeTeam: {homeTeamName} and awayTeam: {awayTeamName} does not exists.")
    {
    }
}