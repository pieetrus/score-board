using Moq;
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
    }
}