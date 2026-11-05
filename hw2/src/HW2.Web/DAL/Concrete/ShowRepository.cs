using HW2.Web.DAL.Abstract;
using HW2.Web.Data;
using HW2.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace HW2.Web.DAL.Concrete;

public class ShowRepository : Repository<Show>, IShowRepository
{
    private readonly DbSet<Show> _shows;

    public ShowRepository(StreamingDbContext context) : base(context)
    {
        _shows = context.Shows;
    }

    public (int show, int movie, int tv) NumberOfShowsByType()
    {
        var counts = _shows
            .GroupBy(show => show.ShowType.ShowTypeIdentifier)
            .Select(group => new { Type = group.Key, Count = group.Count() })
            .ToDictionary(item => item.Type, item => item.Count);

        return (
            _shows.Count(),
            counts.GetValueOrDefault("MOVIE"),
            counts.GetValueOrDefault("SHOW"));
    }

    public Show? ShowWithHighestTMDBPopularity()
    {
        return _shows
            .AsNoTracking()
            .Where(show => show.TmdbPopularity != null)
            .OrderByDescending(show => show.TmdbPopularity)
            .ThenBy(show => show.Id)
            .FirstOrDefault();
    }

    public Show? ShowWithMostIMDBVotes()
    {
        return _shows
            .AsNoTracking()
            .Where(show => show.ImdbVotes != null)
            .OrderByDescending(show => show.ImdbVotes)
            .ThenBy(show => show.Id)
            .FirstOrDefault();
    }

    public IReadOnlyList<string> AvailableGenres()
    {
        return _shows
            .AsNoTracking()
            .SelectMany(show => show.GenreAssignments)
            .Select(assignment => assignment.Genre.GenreString)
            .Distinct()
            .OrderBy(genre => genre)
            .ToList();
    }

    public DirectorShowSummary? DirectorWithMostShows()
    {
        var director = _shows
            .AsNoTracking()
            .SelectMany(
                show => show.Credits.Where(credit => credit.Role.RoleName == "DIRECTOR"),
                (show, credit) => new
                {
                    credit.PersonId,
                    credit.Person.FullName,
                    ShowId = show.Id
                })
            .GroupBy(item => new { item.PersonId, item.FullName })
            .Select(group => new
            {
                group.Key.PersonId,
                group.Key.FullName,
                ShowCount = group.Select(item => item.ShowId).Distinct().Count()
            })
            .OrderByDescending(item => item.ShowCount)
            .ThenBy(item => item.FullName)
            .ThenBy(item => item.PersonId)
            .FirstOrDefault();

        if (director is null)
        {
            return null;
        }

        var showTitles = _shows
            .AsNoTracking()
            .Where(show => show.Credits.Any(credit =>
                credit.PersonId == director.PersonId &&
                credit.Role.RoleName == "DIRECTOR"))
            .OrderBy(show => show.Title)
            .ThenBy(show => show.Id)
            .Select(show => show.Title)
            .ToList();

        return new DirectorShowSummary(director.FullName, showTitles);
    }
}
