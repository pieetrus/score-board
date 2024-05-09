namespace ScoreBoard.Tests
{
    public class ScoreboardService(IMatchRepository matchRepository) : IScoreboardService
    {
        public void StartMatch(string homeTeam, string awayTeam)
        {
            throw new NotImplementedException();
        }
    }
}