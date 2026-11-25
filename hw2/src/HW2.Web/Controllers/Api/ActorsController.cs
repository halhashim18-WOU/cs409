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
    [ProducesResponseType<PagedResultDto<ActorSearchResultDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<PagedResultDto<ActorSearchResultDto>>> Search(
        [FromQuery] ActorSearchRequest request,
        CancellationToken cancellationToken)
    {
        var results = await _personRepository.SearchActorsAsync(
            request.Name,
            request.Page,
            request.PageSize,
            cancellationToken);

        return Ok(results);
    }
}
