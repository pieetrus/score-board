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

        [Theory]
        [InlineData(null, "Away")]
        [InlineData(null, "")]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("Home", null)]
        [InlineData("Home", "")]
        [InlineData("", null)]
        [InlineData("", "Away")]
        public void Create_WithIvalidInputData_ThrowsArgumentException(string homeTeamName, string awayTeamName)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => Match.Create(homeTeamName, awayTeamName));
        }

        [Fact]
        public void Create_WithAwayTeamEqualsHomeTeam_ThrowsArgumentException()
        {
            // Arrange
            var teamName = "TeamName";

            // Act & Assert
            Assert.Throws<ArgumentException>(() => Match.Create(teamName, teamName));
        }
    }
}
