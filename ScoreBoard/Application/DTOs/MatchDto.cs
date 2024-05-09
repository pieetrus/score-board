namespace ScoreBoard.Application.DTOs
{
    public class MatchDto
    {
        public required string HomeTeam { get; set; }
        public required string AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
    }
}
