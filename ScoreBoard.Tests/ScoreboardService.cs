namespace ScoreBoard.Tests
{
    public class ScoreboardService(IMatchRepository matchRepository) : IScoreboardService
    {
        public void StartMatch(string homeTeam, string awayTeam)
        {
            var match = new Match(homeTeam, awayTeam);
            matchRepository.Add(match);
        }
    }
}