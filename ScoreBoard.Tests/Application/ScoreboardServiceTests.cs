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
            _matchRepositoryMock.Setup(repo => repo.GetByTeamNames(homeTeamName, awayTeamName)).Returns(match);

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

            _matchRepositoryMock.Setup(repo => repo.GetByTeamNames(It.IsAny<string>(), It.IsAny<string>())).Returns((Match?)null);

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
            _matchRepositoryMock.Setup(repo => repo.GetByTeamNames(homeTeamName, awayTeamName)).Returns(match);

            // Act & Assert
            Assert.ThrowsAny<ScoreBoardException>(
                () => _service.UpdateScores(homeTeamName, awayTeamName, homeTeamNegativeScore, awayTeamNegativeScore));
        }
    }
}