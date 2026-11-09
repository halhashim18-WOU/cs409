using HW2.Web.Models;

namespace HW2.Web.DAL.Abstract;

public interface IShowRepository : IRepository<Show>
{
    (int show, int movie, int tv) NumberOfShowsByType();

    Show? ShowWithHighestTMDBPopularity();

    Show? ShowWithMostIMDBVotes();

    IReadOnlyList<string> AvailableGenres();

    DirectorShowSummary? DirectorWithMostShows();
}
