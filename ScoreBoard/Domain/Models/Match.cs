using ScoreBoard.Domain.Exceptions;

namespace ScoreBoard.Domain.Models
{
    public class Match
    {
        public string HomeTeam { get; private set; }
        public string AwayTeam { get; private set; }
        public int HomeScore { get; private set; }
        public int AwayScore { get; private set; }
        public DateTime StartTime { get; private set; }  // Added to track when the match started

        private Match(string homeTeam, string awayTeam, DateTime startTime)
        {
            HomeTeam = homeTeam;
            AwayTeam = awayTeam;
            HomeScore = 0;
            AwayScore = 0;
            StartTime = startTime;
        }

        public static Match Create(string homeTeam, string awayTeam, DateTime? startTime = null)
        {
            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            {
                throw new TeamCannotBeNullOrEmptyException();
            }

            if (homeTeam.Equals(awayTeam))
            {
                throw new HomeTeamCannotBeEqualToAwayTeamException();
            }

            return new Match(homeTeam, awayTeam, startTime ?? DateTime.UtcNow);
        }

        public void UpdateScores(int newHomeScore, int newAwayScore)
        {
            if (newHomeScore < 0 || newAwayScore < 0)
                throw new ScoresMustBeNonNegativeException();

            HomeScore = newHomeScore;
            AwayScore = newAwayScore;
        }
    }
}