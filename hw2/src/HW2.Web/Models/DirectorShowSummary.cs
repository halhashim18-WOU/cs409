namespace HW2.Web.Models;

// Combines a director and ordered titles that do not map to one database table.
public sealed record DirectorShowSummary(
    string DirectorName,
    IReadOnlyList<string> ShowTitles);
