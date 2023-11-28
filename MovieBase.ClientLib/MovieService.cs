using MovieBase.Common;
using MovieBase.Common.Interfaces;
using System.Net.Http.Json;

namespace MovieBase.ClientLib;

public class MovieService : IMovieService
{


    public async Task<IEnumerable<Movie>> GetPageAsync(int pageSize, int pageNo)
    {
        var client = new HttpClient();
        var movies = await client.GetFromJsonAsync<IEnumerable<Movie>>($"https://localhost:7267/movies/list/{pageSize}/{pageNo}");
        return movies!.ToList();
    }
}
