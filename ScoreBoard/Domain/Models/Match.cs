namespace ScoreBoard.Domain.Models
{
    public class Match
    {
        public string HomeTeam { get; private set; }
        public string AwayTeam { get; private set; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }

        private Match(string homeTeam, string awayTeam)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = 0;
            AwayScore = 0;
        }

        public static Match Create(string homeTeam, string awayTeam)
        {
            if (string.IsNullOrEmpty(homeTeam))
            {
                throw new ArgumentException("Home team is required");
            }

            if (string.IsNullOrEmpty(awayTeam))
            {
                throw new ArgumentException("Away team is required");
            }

            return new Match(homeTeam, awayTeam);
        }
    }
}