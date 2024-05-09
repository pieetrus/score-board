using Moq;
using ScoreBoard.Application.Exceptions;
using ScoreBoard.Application.Interfaces;
using ScoreBoard.Application.Services;
using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Shared.Exceptions;
using Match = ScoreBoard.Domain.Models.Match;

namespace ScoreBoard.Tests.Application
{
    public class ScoreboardServiceTests
    {
        private readonly Mock<IMatchRepository> _matchRepositoryMock;
        private readonly IScoreboardService _service;

        public ScoreboardServiceTests()
        {
            _matchRepositoryMock = new Mock<IMatchRepository>();
            _service = new ScoreboardService(_matchRepositoryMock.Object);
        }

        [Fact]
        public void StartMatch_WithValidInputData_CreateMatchWithValidDataAndZeroScores()
        {
            // Arrange
            var homeTeamName = "Home";
            var awayTeamName = "Away";
            Match? createdMatch = null;

            // Act
            _matchRepositoryMock.Setup(m => m.Add(It.IsAny<Match>()))
                .Callback<Match>(match =>
                {
                    createdMatch = match;
                });

            _service.StartMatch(homeTeamName, awayTeamName);

            // Assert
            _matchRepositoryMock.Verify(m => m.Add(It.IsAny<Match>()), Times.Once);

            Assert.Equal(homeTeamName, createdMatch!.HomeTeam);
            Assert.Equal(awayTeamName, createdMatch.AwayTeam);
            Assert.Equal(0, createdMatch.HomeScore);
            Assert.Equal(0, createdMatch.AwayScore);
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
        public void StartMatch_WithIvalidInputData_ThrowsAnyScoreBoardException(string homeTeamName, string awayTeamName)
        {
            // Act & Assert
            var exception = Assert.ThrowsAny<ScoreBoardException>(() => _service.StartMatch(homeTeamName, awayTeamName));
        }

        [Fact]
        public void UpdateScores_WhenMatchExists_UpdatesScoresCorrectly()
        {
            // Arrange
            var homeTeamName = "HomeTeam";
            var awayTeamName = "AwayTeam";
            var homeTeamScore = 3;
            var awayTeamScore = 2;

            var match = Match.Create(homeTeamName, awayTeamName);
            _matchRepositoryMock.Setup(repo => repo.GetMatchByTeamNames(homeTeamName, awayTeamName)).Returns(match);

            // Act
            _service.UpdateScores(homeTeamName, awayTeamName, homeTeamScore, awayTeamScore);

            // Assert
            Assert.Equal(homeTeamScore, match.HomeScore);
            Assert.Equal(awayTeamScore, match.AwayScore);
            _matchRepositoryMock.Verify(repo => repo.Update(match), Times.Once);
        }

        [Fact]
        public void UpdateScores_WhenMatchDoesNotExist_ThrowsMatchNotFoundException()
        {
            // Arrange
            var homeTeamName = "NonExistentHome";
            var awayTeamName = "NonExistentAway";
            var homeTeamScore = 1;
            var awayTeamScore = 1;

            _matchRepositoryMock.Setup(repo => repo.GetMatchByTeamNames(It.IsAny<string>(), It.IsAny<string>())).Returns((Match?)null);

            // Act & Assert
            Assert.Throws<MatchNotFoundException>(
                () => _service.UpdateScores(homeTeamName, awayTeamName, homeTeamScore, awayTeamScore));

            _matchRepositoryMock.Verify(repo => repo.Update(It.IsAny<Match>()), Times.Never);
        }

        [Fact]
        public void UpdateScores_WithNegativeScores_ThrowsAnyScoreBoardException()
        {
            // Arrange
            // Arrange
            var homeTeamName = "HomeTeam";
            var awayTeamName = "AwayTeam";
            var homeTeamNegativeScore = -1;
            var awayTeamNegativeScore = -1;

            var match = Match.Create(homeTeamName, awayTeamName);
            _matchRepositoryMock.Setup(repo => repo.GetMatchByTeamNames(homeTeamName, awayTeamName)).Returns(match);

            // Act & Assert
            Assert.ThrowsAny<ScoreBoardException>(
                () => _service.UpdateScores(homeTeamName, awayTeamName, homeTeamNegativeScore, awayTeamNegativeScore));
        }

        [Fact]
        public void FinishMatch_WhenMatchExists_RemovesMatchFromRepository()
        {
            // Arrange
            var homeTeamName = "HomeTeam";
            var awayTeamName = "AwayTeam";

            var match = Match.Create(homeTeamName, awayTeamName);
            _matchRepositoryMock.Setup(repo => repo.GetMatchByTeamNames(homeTeamName, awayTeamName)).Returns(match);

            // Act
            _service.FinishMatch(homeTeamName, awayTeamName);

            // Assert
            _matchRepositoryMock.Verify(repo => repo.Remove(match), Times.Once);
        }

        [Fact]
        public void FinishMatch_WhenMatchDoesNotExist_ThrowsMatchNotFoundException()
        {
            // Arrange
            var homeTeamName = "NonExistentHome";
            var awayTeamName = "NonExistentAway";

            _matchRepositoryMock.Setup(repo => repo.GetMatchByTeamNames(It.IsAny<string>(), It.IsAny<string>())).Returns((Match?)null);

            // Act & Assert
            Assert.Throws<MatchNotFoundException>(
                () => _service.FinishMatch(homeTeamName, awayTeamName));

            _matchRepositoryMock.Verify(repo => repo.Remove(It.IsAny<Match>()), Times.Never);
        }

        [Fact]
        public void GetSummary_ReturnsMatchesOrderedByTotalScoreAndStartTime()
        {
            // Arrange
            var now = DateTime.UtcNow;
            var matches = new List<Match>
            {
                Match.Create("Team1", "Team2", now.AddHours(-4)), // Started 4 hours ago
                Match.Create("Team3", "Team4", now.AddHours(-3)), // Started 3 hours ago
                Match.Create("Team5", "Team6", now.AddHours(-2)), // Started 2 hours ago
                Match.Create("Team7", "Team8", now.AddHours(-1))  // Started 1 hour ago
            };

            matches[0].UpdateScores(3, 2); // Total 5
            matches[1].UpdateScores(1, 4); // Total 5, started later than match 0
            matches[2].UpdateScores(2, 3); // Total 5, started even later
            matches[3].UpdateScores(0, 7); // Total 7, most recent but highest score

            _matchRepositoryMock.Setup(repo => repo.GetAll()).Returns(matches);

            // Act
            var summary = _service.GetSummary().ToList();

            // Assert
            Assert.Equal(matches.Count, summary.Count);
            Assert.Equal(7, summary[0].HomeScore + summary[0].AwayScore); // Team7 vs Team8
            Assert.Equal(5, summary[1].HomeScore + summary[1].AwayScore); // Team5 vs Team6
            Assert.Equal(5, summary[2].HomeScore + summary[2].AwayScore); // Team3 vs Team4
            Assert.Equal(5, summary[3].HomeScore + summary[3].AwayScore); // Team1 vs Team2
        }
    }
}