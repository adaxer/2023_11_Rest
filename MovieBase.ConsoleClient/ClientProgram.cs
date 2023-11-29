using Microsoft.AspNetCore.JsonPatch;
using MovieBase.ClientLib;
using MovieBase.Common;
using System.Net.Http.Json;

namespace MovieBase.ConsoleClient;

internal class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            Console.Write("Not found");
            var result = await new HttpClient().GetAsync("https://localhost:7267/something");

            Console.WriteLine("Movieliste");
            //Console.ReadLine();
            var service = new MovieService();
            var movies = await service.GetMoviePage(20, 0);

            foreach (var item in movies)
            {
                Console.WriteLine(item.Title);
            }

            Console.WriteLine("\n\nEinzelnes Movie");
            //Console.ReadLine();
            var movie = await service.GetMovie(33);
            Console.WriteLine(movie.Title);

            Console.WriteLine("\n\nAdd Movie");
            Console.ReadLine();
            await service.AddMovie(new Movie { Title = "Stargate", Director="Whoever" }, CancellationToken.None);

            Console.WriteLine("\n\nPut");
            Console.ReadLine();

            movie = await service.UpdateMovie(new Movie
            {
                Id = 1,
                Title = "Blade Runner 2048",
                Director = "Ridley",
                Released = DateOnly.FromDateTime(DateTime.Now.AddYears(-5))
            }, CancellationToken.None);
            Console.WriteLine($"{movie.Title}, {movie.Director}, {movie.Released}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();

    }
}
