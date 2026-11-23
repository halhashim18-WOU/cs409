using HW2.Web.DAL.Abstract;
using HW2.Web.Data;
using HW2.Web.Models;
using HW2.Web.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace HW2.Web.DAL.Concrete;

public class PersonRepository : Repository<Person>, IPersonRepository
{
    private readonly DbSet<Person> _people;

    public PersonRepository(StreamingDbContext context) : base(context)
    {
        _people = context.People;
    }

    public async Task<IReadOnlyList<ActorSearchResultDto>> SearchActorsAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return [];
        }

        var searchTerm = name.Trim();

        // Search Person directly so newly added actors with no credits are still returned.
        return await _people
            .AsNoTracking()
            .Where(person => person.FullName.Contains(searchTerm))
            .OrderBy(person => person.FullName == searchTerm ? 0 : 1)
            .ThenBy(person => person.FullName)
            .ThenBy(person => person.Id)
            .Select(person => new ActorSearchResultDto(
                person.Id,
                person.JustWatchPersonId,
                person.FullName))
            .Take(20)
            .ToListAsync(cancellationToken);
    }
}
