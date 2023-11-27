using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBase.Common;
using MovieBase.Data;
using System.Net.Mime;

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

    [HttpGet("[action]/{pageSize}/{pageNo}", Name = "MovieList")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    public async Task<IActionResult> List(int pageSize, int pageNo)
    {
        var data = await _context.Movies.Skip(pageNo * pageSize).Take(pageSize).ToArrayAsync();
        return Ok(data);
    }

    [HttpGet("{id}", Name = "Movie")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    public async Task<IActionResult> One(int id)
    {
        var data = await _context.Movies.FindAsync(id);
        return data == null
            ? NotFound()
            : Ok(data);
    }
    //public async Task<IActionResult> One(string id)
    //{
    //    if (int.TryParse(id, out var realId))
    //    {
    //        var data = await _context.Movies.FindAsync(realId);
    //        return data == null
    //            ? NotFound()
    //            : Ok(data);
    //    }
    //    return BadRequest("An int is expected");
    //}

    [HttpPatch("{id}")]
    public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Movie> patchDoc)
    {
        try
        {
            var movie = await _context.Movies.FindAsync(id);
            patchDoc.ApplyTo(movie!, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.SaveChangesAsync();
            return new ObjectResult(movie);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}

public class Result<T>
{
    public bool Succeeded { get; set; }
    public T? Payload { get; set; }
    public string? Message { get; set; }
}
