namespace ScoreBoard.Application.Interfaces
{
    public interface IScoreboardService
    {
        void StartMatch(string homeTeam, string awayTeam);
        void UpdateScores(string homeTeamName, string awayTeamName, int homeTeamScore, int awayTeamScore);
    }
}