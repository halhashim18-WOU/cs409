using System.ComponentModel.DataAnnotations;

namespace HW2.Web.Models.Requests;

public sealed class ActorSearchRequest
{
    [Required(ErrorMessage = "An actor name is required.")]
    public string Name { get; init; } = string.Empty;

    [Range(1, int.MaxValue, ErrorMessage = "Page must be at least 1.")]
    public int Page { get; init; } = 1;

    [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100.")]
    public int PageSize { get; init; } = 20;
}
