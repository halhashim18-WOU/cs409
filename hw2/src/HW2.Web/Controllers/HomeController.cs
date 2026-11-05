using System.Diagnostics;
using HW2.Web.DAL.Abstract;
using Microsoft.AspNetCore.Mvc;
using HW2.Web.Models;
using HW2.Web.Models.ViewModels;

namespace HW2.Web.Controllers;

public class HomeController : Controller
{
    // Provides the database queries needed by the info page.
    private readonly IShowRepository _showRepository;

    public HomeController(IShowRepository showRepository)
    {
        _showRepository = showRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Info()
    {
        // Gather the query results here so the view only handles presentation.
        var counts = _showRepository.NumberOfShowsByType();
        var mostPopularShow = _showRepository.ShowWithHighestTMDBPopularity();
        var mostVotedShow = _showRepository.ShowWithMostIMDBVotes();
        var topDirector = _showRepository.DirectorWithMostShows();

        var viewModel = new InfoViewModel
        {
            TotalShows = counts.show,
            MovieCount = counts.movie,
            TvShowCount = counts.tv,
            HighestTmdbPopularityShowTitle = mostPopularShow?.Title,
            HighestTmdbPopularity = mostPopularShow?.TmdbPopularity,
            MostImdbVotesShowTitle = mostVotedShow?.Title,
            MostImdbVotes = mostVotedShow?.ImdbVotes,
            Genres = _showRepository.AvailableGenres(),
            TopDirectorName = topDirector?.DirectorName,
            TopDirectorShowTitles = topDirector?.ShowTitles ?? []
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
