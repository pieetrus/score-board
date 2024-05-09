using ScoreBoard.Shared.Exceptions;

namespace ScoreBoard.Application.Exceptions
{
    public class MatchNotFoundException(string homeTeamName, string awayTeamName)
        : ScoreBoardException($"Match with {homeTeamName} and {awayTeamName} was not found.")
    {
    }
}
