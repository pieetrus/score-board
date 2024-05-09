# Live Football World Cup Scoreboard Library

## Overview

The Live Football World Cup Scoreboard is a simple library designed to manage football matches during the World Cup. It provides functionalities to start a new match, update scores, finish a match, and retrieve a summary of ongoing matches based on the total score and most recent activity.

## Getting Started

### Prerequisites

- .NET 8
- Any preferred IDE that supports C# (e.g., Visual Studio, VSCode)

### Installation

Clone the repository to your local machine:

```
git clone https://github.com/pieetrus/score-board
```

### Usage

To use the scoreboard in your project, reference the library and follow the example below to manipulate match data:

```
var scoreboardService = new ScoreboardService(new MatchRepository());

// Starting a match
scoreboardService.StartMatch("HomeTeamName", "AwayTeamName");

// Updating scores
scoreboardService.UpdateScores("HomeTeamName", "AwayTeamName", 2, 3);

// Finishing a match
scoreboardService.FinishMatch("HomeTeamName", "AwayTeamName");

// Getting summary of matches
var summary = scoreboardService.GetSummary();

```

### Assumptions

1. Matches that was finished does not need to be stored.
2. Only one match of teams with the same name could be ongoing.
3. No UI for interaction with library is required.
4. Summary doesn't have to be returned in format provided as example in documentation.

### Potential improvements

#### Data Persistence

Database Integration: Transition from an in-memory store to a database to ensure data persistence across system restarts and crashes, which would also support more complex queries and long-term data storage.

#### Domain Driven Design

- Use factory for Match creation and Score update - for now it could break SRP principle
- Use ValueObjects
- Add identifier to Match - now they are diffrentiate based on home and away team names
- Use Events - for better testing

#### Add more abstractions

Abstraction could be added from repository for easier database or other storage implementation

#### Asynchronous operations

I did not add it due to in-memory storage solution was used.
