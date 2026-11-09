namespace HW2.Web.Models.ViewModels;

// Contains the prepared data displayed on the info page.
public sealed class InfoViewModel
{
    public int TotalShows { get; init; }

    public int MovieCount { get; init; }

    public int TvShowCount { get; init; }

    // Nullable ranking results allow the page to handle an empty database.
    public string? HighestTmdbPopularityShowTitle { get; init; }

    public double? HighestTmdbPopularity { get; init; }

    public string? MostImdbVotesShowTitle { get; init; }

    public double? MostImdbVotes { get; init; }

    // Empty collection defaults keep the Razor loops safe.
    public IReadOnlyList<string> Genres { get; init; } = [];

    public string? TopDirectorName { get; init; }

    public IReadOnlyList<string> TopDirectorShowTitles { get; init; } = [];
}
