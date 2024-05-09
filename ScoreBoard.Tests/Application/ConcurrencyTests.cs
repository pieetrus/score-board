using ScoreBoard.Application.Interfaces;
using ScoreBoard.Application.Services;
using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Domain.Models;
using ScoreBoard.Infrastructure.Repositories;

namespace ScoreBoard.Tests.Application;

public class ConcurrencyTests
{
    private IScoreboardService _service;
    private IMatchRepository _repository;

    public ConcurrencyTests()
    {
        // Assume MatchRepository is designed to be thread-safe.
        _repository = new MatchRepository(); // Real implementation
        _service = new ScoreboardService(_repository); // Real service
    }

    [Fact]
    public async Task UpdateScores_ConcurrentUpdates_ShouldHandleCorrectly()
    {
        // Arrange
        var homeTeamName = "HomeTeam";
        var awayTeamName = "AwayTeam";
        var match = Match.Create(homeTeamName, awayTeamName);
        _repository.Add(match);

        // Act
        Task update1 = Task.Run(() => _service.UpdateScores(homeTeamName, awayTeamName, 10, 5));
        Task update2 = Task.Run(() => _service.UpdateScores(homeTeamName, awayTeamName, 15, 10));
        await Task.WhenAll(update1, update2);

        // Assert
        var updatedMatch = _repository.GetMatchByTeamNames(homeTeamName, awayTeamName);
        Assert.NotNull(updatedMatch);
        Assert.True(updatedMatch.HomeScore == 15 && updatedMatch.AwayScore == 10, "Scores should reflect the last update applied.");
    }
}