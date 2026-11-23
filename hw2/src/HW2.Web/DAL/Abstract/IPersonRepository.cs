using HW2.Web.Models;
using HW2.Web.Models.Dtos;

namespace HW2.Web.DAL.Abstract;

public interface IPersonRepository : IRepository<Person>
{
    Task<IReadOnlyList<ActorSearchResultDto>> SearchActorsAsync(
        string name,
        CancellationToken cancellationToken = default);
}
