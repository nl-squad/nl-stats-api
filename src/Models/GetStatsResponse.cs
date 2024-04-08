namespace Nl.Stats.Api.Models;

public record GetStatsResponse(
    string Login,
    int Rank,
    int Kills
);