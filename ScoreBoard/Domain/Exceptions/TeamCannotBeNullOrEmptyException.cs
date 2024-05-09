using ScoreBoard.Shared.Exceptions;

namespace ScoreBoard.Domain.Exceptions
{
    public class TeamCannotBeNullOrEmptyException() : ScoreBoardException("Team cannot be null or empty.")
    {
    }
}
