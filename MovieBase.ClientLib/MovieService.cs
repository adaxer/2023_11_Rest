using MovieBase.Common;
using MovieBase.Common.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace MovieBase.ClientLib;

public class MovieService : IMovieService
{
    private string _baseUrl = "https://localhost:7267";
    private HttpClient _client = new HttpClient();

    public async Task<IEnumerable<Movie>> GetMoviePage(int pageSize, int pageNo)
    {
        var movies = await _client.GetFromJsonAsync<IEnumerable<Movie>>($"{_baseUrl}/movies/list/{pageSize}/{pageNo}");
        return movies!.ToList();
    }

    public async Task<Movie> GetMovie(int id)
    {
        var movie = await _client.GetFromJsonAsync<Movie>($"{_baseUrl}/movies/{id}");
        return movie!;
    }

    public async Task AddMovie(Movie movie, CancellationToken token)
    {
        var result = await _client.PostAsJsonAsync<Movie>($"{_baseUrl}/movies", movie, token);
        Trace.WriteLine($"Status {result.StatusCode}");
        foreach (var item in result.Headers)
        {
            var value = string.Join(",", item.Value.ToArray());
            Trace.WriteLine($"Header {item.Key}: {value}");
        }
        var body = await result.Content.ReadAsStringAsync();
        Trace.WriteLine($"Body: {body}");
    }

    public async Task<Movie> UpdateMovie(Movie movie, CancellationToken token)
    {
        var result = await _client.PutAsJsonAsync<Movie>($"{_baseUrl}/movies", movie, token);
        if(result.IsSuccessStatusCode)
        {
            var json = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Movie>(json)!;
        }
        throw new HttpRequestException("Could not update");
    }
}
