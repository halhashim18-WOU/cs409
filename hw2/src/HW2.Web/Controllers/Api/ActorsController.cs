using HW2.Web.DAL.Abstract;
using HW2.Web.Models.Dtos;
using HW2.Web.Models.Requests;
using Microsoft.AspNetCore.Mvc;

namespace HW2.Web.Controllers.Api;

[ApiController]
[Route("api/search")]
public class ActorsController : ControllerBase
{
    private readonly IPersonRepository _personRepository;

    public ActorsController(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    [HttpGet("actor")]
    [ProducesResponseType<IReadOnlyList<ActorSearchResultDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<ActorSearchResultDto>>> Search(
        [FromQuery] ActorSearchRequest request,
        CancellationToken cancellationToken)
    {
        var results = await _personRepository.SearchActorsAsync(
            request.Name,
            cancellationToken);

        return Ok(results);
    }
}
