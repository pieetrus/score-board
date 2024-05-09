using ScoreBoard.Domain.Exceptions;

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
            if (string.IsNullOrEmpty(homeTeam) || string.IsNullOrEmpty(awayTeam))
            {
                throw new TeamCannotBeNullOrEmptyException();
            }

            if (homeTeam.Equals(awayTeam))
            {
                throw new HomeTeamCannotBeEqualToAwayTeamException();
            }

            return new Match(homeTeam, awayTeam);
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