using ScoreBoard.Domain.Exceptions;
using ScoreBoard.Domain.Models;

namespace ScoreBoard.Tests.Domain
{
    public class MatchTests
    {
        [Fact]
        public void Create_WithValidInputData_CreateMatchWithValidDataAndZeroScores()
        {
            // Arrange
            var homeTeamName = "Home";
            var awayTeamName = "Away";

            // Act
            var match = Match.Create(homeTeamName, awayTeamName);

            // Assert
            Assert.Equal(homeTeamName, match.HomeTeam);
            Assert.Equal(awayTeamName, match.AwayTeam);
            Assert.Equal(0, match.HomeScore);
            Assert.Equal(0, match.AwayScore);
        }

        [Fact]
        public void Match_Creation_SetsStartTimeToCurrentTime()
        {
            // Act
            var beforeCreation = DateTime.UtcNow;
            var match = Match.Create("HomeTeam", "AwayTeam");
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.True(match.StartTime >= beforeCreation && match.StartTime <= afterCreation);
        }

        [Fact]
        public void CreateMatch_WithDifferentTimeZones_ShouldHandleTimeZoneCorrectly()
        {
            // Arrange
            var homeTeamName = "HomeTeam";
            var awayTeamName = "AwayTeam";
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);

            // Act
            var match = Match.Create(homeTeamName, awayTeamName, localTime);

            // Assert
            Assert.Equal(localTime, match.StartTime);
        }

        [Theory]
        [InlineData(null, "Away")]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("Home", null)]
        [InlineData("Home", "")]
        [InlineData("", null)]
        [InlineData("", "Away")]
        public void Create_WithIvalidInputData_ThrowsTeamCannotBeNullOrEmptyException(string homeTeamName, string awayTeamName)
        {
            // Act & Assert
            var exception = Assert.Throws<TeamCannotBeNullOrEmptyException>(()
                => Match.Create(homeTeamName, awayTeamName));
        }

        [Fact]
        public void Create_WithAwayTeamEqualsHomeTeam_ThrowsHomeTeamCannotBeEqualToAwayTeamException()
        {
            // Arrange
            var teamName = "TeamName";

            // Act & Assert
            var exception = Assert.Throws<HomeTeamCannotBeEqualToAwayTeamException>(
                () => Match.Create(teamName, teamName));
        }

        [Fact]
        public void UpdateScores_WithValidScores_UpdatesSuccessfully()
        {
            // Arrange
            var match = Match.Create("HomeTeam", "AwayTeam");
            int newHomeScore = 3;
            int newAwayScore = 2;

            // Act
            match.UpdateScores(newHomeScore, newAwayScore);

            // Assert
            Assert.Equal(newHomeScore, match.HomeScore);
            Assert.Equal(newAwayScore, match.AwayScore);
        }

        [Fact]
        public void UpdateScores_WithNegativeHomeScore_ThrowsScoresMustBeNonNegativeException()
        {
            // Arrange
            var match = Match.Create("HomeTeam", "AwayTeam");
            int newHomeScore = -1;
            int newAwayScore = 1;

            // Act & Assert
            var exception = Assert.Throws<ScoresMustBeNonNegativeException>(
                () => match.UpdateScores(newHomeScore, newAwayScore));
        }

        [Fact]
        public void UpdateScores_WithNegativeAwayScore_ThrowsScoresMustBeNonNegativeException()
        {
            // Arrange
            var match = Match.Create("HomeTeam", "AwayTeam");
            int newHomeScore = 1;
            int newAwayScore = -1;

            // Act & Assert
            var exception = Assert.Throws<ScoresMustBeNonNegativeException>(
                () => match.UpdateScores(newHomeScore, newAwayScore));
        }
    }
}
