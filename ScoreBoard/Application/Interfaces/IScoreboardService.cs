namespace ScoreBoard.Application.Interfaces
{
    public interface IScoreboardService
    {
        void StartMatch(string homeTeam, string awayTeam);
    }
}