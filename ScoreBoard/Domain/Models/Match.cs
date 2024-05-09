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

        public static Match Create(string homeTeamName, string awayTeamName, DateTime? startTime = null)
        {
            if (string.IsNullOrEmpty(homeTeamName) || string.IsNullOrEmpty(awayTeamName))
            {
                throw new TeamCannotBeNullOrEmptyException();
            }

            if (homeTeamName.Equals(awayTeamName))
            {
                throw new HomeTeamCannotBeEqualToAwayTeamException();
            }

            return new Match(homeTeamName, awayTeamName, startTime ?? DateTime.UtcNow);
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