using ScoreBoard.Shared.Exceptions;

namespace ScoreBoard.Domain.Exceptions
{
    public class HomeTeamCannotBeEqualToAwayTeamException() : ScoreBoardException("Home team cannot be equal to away team.")
    {
    }
}
