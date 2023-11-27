using Microsoft.AspNetCore.Mvc;
using MovieBase.Common;
using MovieBase.Data;

namespace MovieBase.Api.Controllers;
[ApiController]
[Route("[controller]")]
public class MoviesController : ControllerBase
{
    private readonly ILogger<MoviesController> _logger;
    private readonly MovieContext _context;

    public MoviesController(ILogger<MoviesController> logger, MovieContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet("[action]", Name = "MovieList")]
    public IEnumerable<Movie> List()
    {
        return _context.Movies.Take(10).ToArray();
    }
}
