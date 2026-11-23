using System.ComponentModel.DataAnnotations;

namespace HW2.Web.Models.Requests;

public sealed class ActorSearchRequest
{
    [Required(ErrorMessage = "An actor name is required.")]
    public string Name { get; init; } = string.Empty;
}
