namespace ScoreBoard.Shared.Exceptions
{
    public abstract class ScoreBoardException : Exception
    {
        protected ScoreBoardException(string message) : base(message)
        {
        }
    }
}
