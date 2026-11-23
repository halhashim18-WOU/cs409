namespace HW2.Web.Models.Dtos;

// Contains only the actor information the search endpoint needs to return.
public sealed record ActorSearchResultDto(
    int Id,
    int JustWatchPersonId,
    string FullName);
