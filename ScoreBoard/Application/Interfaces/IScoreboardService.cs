namespace ScoreBoard.Application.Interfaces
{
    public interface IScoreboardService
    {
        void FinishMatch(string homeTeamName, string awayTeamName);
        void StartMatch(string homeTeam, string awayTeam);
        void UpdateScores(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore);
    }
}