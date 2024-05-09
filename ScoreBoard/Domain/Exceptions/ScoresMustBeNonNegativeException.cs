using ScoreBoard.Shared.Exceptions;

namespace ScoreBoard.Domain.Exceptions
{
    public class ScoresMustBeNonNegativeException() : ScoreBoardException("Scores must be non-negative.")
    {
    }
}
