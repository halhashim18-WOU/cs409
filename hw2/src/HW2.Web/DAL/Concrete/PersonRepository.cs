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

    public async Task<PagedResultDto<ActorSearchResultDto>> SearchActorsAsync(
        string name,
        int page,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return new PagedResultDto<ActorSearchResultDto>([], page, pageSize, 0);
        }

        var searchTerm = string.Join(' ', name.Split(
            ' ',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries));
        var namePrefix = searchTerm + " ";

        // Match an exact name or the beginning of a longer name, not text embedded in the middle.
        var query = _people
            .AsNoTracking()
            .Where(person =>
                person.FullName == searchTerm ||
                person.FullName.StartsWith(namePrefix));

        var totalCount = await query.CountAsync(cancellationToken);

        // Full-name ordering sorts the last-name portion alphabetically for the same first name.
        var actors = await query
            .OrderBy(person => person.FullName)
            .ThenBy(person => person.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(person => new ActorSearchResultDto(
                person.Id,
                person.JustWatchPersonId,
                person.FullName))
            .ToListAsync(cancellationToken);

        return new PagedResultDto<ActorSearchResultDto>(
            actors,
            page,
            pageSize,
            totalCount);
    }
}
