namespace HW2.Web.Models;

public sealed record DirectorShowSummary(
    string DirectorName,
    IReadOnlyList<string> ShowTitles);
