using Moq;

namespace ScoreBoard.Tests
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
        public void StartMatch_CreatesNewMatch()
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
    }
}