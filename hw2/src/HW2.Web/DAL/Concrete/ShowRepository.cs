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
}
