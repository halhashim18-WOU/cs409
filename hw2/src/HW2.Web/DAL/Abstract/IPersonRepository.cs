using HW2.Web.Models;
using HW2.Web.Models.Dtos;

namespace HW2.Web.DAL.Abstract;

public interface IPersonRepository : IRepository<Person>
{
    Task<PagedResultDto<ActorSearchResultDto>> SearchActorsAsync(
        string name,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}
