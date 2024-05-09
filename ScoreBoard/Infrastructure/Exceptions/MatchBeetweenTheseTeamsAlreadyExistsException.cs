using ScoreBoard.Shared.Exceptions;

namespace ScoreBoard.Infrastructure.Exceptions
{
    public class MatchBeetweenTheseTeamsAlreadyExistsException(string homeTeamName, string awayTeamName)
        : ScoreBoardException($"Match between {homeTeamName} and {awayTeamName} already exists.")
    {
    }
}