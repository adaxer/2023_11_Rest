using MovieBase.ClientLib;
using MovieBase.Common;
using System.Net.Http.Json;

namespace MovieBase.ConsoleClient;

internal class Program
{
    static async Task Main(string[] args)
    {
        Console.ReadLine();
        var service = new MovieService();
        var movies = await service.GetPageAsync(20, 0);
        Console.WriteLine(movies.ToList().FirstOrDefault()?.Title);

        Console.ReadLine();
    }
}
