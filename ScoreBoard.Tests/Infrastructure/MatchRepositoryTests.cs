using ScoreBoard.Domain.Interfaces;
using ScoreBoard.Domain.Models;
using ScoreBoard.Infrastructure.Repositories;

namespace ScoreBoard.Tests.Infrastructure;

public class MatchRepositoryTests
{
    private readonly IMatchRepository _repository;

    public MatchRepositoryTests()
    {
        _repository = new MatchRepository();
    }

    [Fact]
    public void Add_AddsMatchToList()
    {
        // Arrange
        var match = Match.Create("HomeTeam", "AwayTeam");

        // Act
        _repository.Add(match);

        // Assert
        var allMatches = _repository.GetAll().ToList();
        Assert.Contains(match, allMatches);
    }

    [Fact]
    public void GetAll_ReturnsAllMatches()
    {
        // Arrange
        var match1 = Match.Create("HomeTeam1", "AwayTeam1");
        var match2 = Match.Create("HomeTeam2", "AwayTeam2");
        _repository.Add(match1);
        _repository.Add(match2);

        // Act
        var matches = _repository.GetAll();

        // Assert
        Assert.Equal(2, matches.Count());
        Assert.Contains(match1, matches);
        Assert.Contains(match2, matches);
    }

    [Fact]
    public void GetByTeamNames_ReturnsCorrectMatch()
    {
        // Arrange
        var homeTeamName = "HomeTeam";
        var awayTeamName = "AwayTeam";
        var match = Match.Create(homeTeamName, awayTeamName);
        _repository.Add(match);

        // Act
        var foundMatch = _repository.GetByTeamNames(homeTeamName, awayTeamName);

        // Assert
        Assert.NotNull(foundMatch);
        Assert.Equal(homeTeamName, foundMatch.HomeTeam);
        Assert.Equal(awayTeamName, foundMatch.AwayTeam);
    }

    [Fact]
    public void Remove_RemovesMatchFromList()
    {
        // Arrange
        var match = Match.Create("HomeTeam", "AwayTeam");
        _repository.Add(match);

        // Act
        _repository.Remove(match);

        // Assert
        Assert.DoesNotContain(match, _repository.GetAll());
    }

    [Fact]
    public void Update_UpdatesMatchInfo()
    {
        // Arrange
        var match = Match.Create("HomeTeam", "AwayTeam");
        _repository.Add(match);

        var updatedHomeTeamName = "HomeTeamUpdated";
        var updatedAwayTeamName = "AwayTeamUpdated";
        var updatedHomeTeamScore = 3;
        var updatedAwayTeamScore = 6;

        var updatedMatch = Match.Create(updatedHomeTeamName, updatedAwayTeamName);
        updatedMatch.UpdateScores(updatedHomeTeamScore, updatedAwayTeamScore);

        // Act
        _repository.Update(updatedMatch);

        // Assert
        var foundMatch = _repository.GetByTeamNames(updatedHomeTeamName, updatedAwayTeamName);
        Assert.Equal(updatedHomeTeamScore, foundMatch!.HomeScore);
        Assert.Equal(updatedAwayTeamScore, foundMatch.AwayScore);
    }
}