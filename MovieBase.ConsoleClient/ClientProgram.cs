using Microsoft.AspNetCore.JsonPatch;
using MovieBase.ClientLib;
using MovieBase.Common;
using MovieBase.Common.Interfaces;
using System.Net.Http.Json;

namespace MovieBase.ConsoleClient;

internal class Program
{
    static IMovieService service = new MovieService();
    static async Task Main(string[] args)
    {
        try
        {
            Console.Write("Not found");
            Console.ReadLine();
            var result = await new HttpClient().GetAsync("https://localhost:7267/something");

            Console.WriteLine("\n\nMovieliste");
            var movies = await service.GetMoviePage(20, 0);

            foreach (var item in movies)
            {
                Console.WriteLine(item.Info);
            }

            Console.WriteLine("\n\nEinzelnes Movie");
            Console.ReadLine();
            if (!await TryLoadMovie())
            {
                await RegisterAndLogin();
                await TryLoadMovie();
            }

            Console.WriteLine("\n\nAdd Movie");
            Console.ReadLine();
            await service.AddMovie(new Movie { Title = "Stargate", Director = "Whoever" }, CancellationToken.None);

            Console.WriteLine("\n\nPut");
            Console.ReadLine();

            var success = await service.UpdateMovie(new Movie
            {
                Id = 1,
                Title = "Blade Runner 2048",
                Director = "Ridley",
                Released = DateOnly.FromDateTime(DateTime.Now.AddYears(-5))
            }, CancellationToken.None);
            Console.WriteLine($"Success? {success}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        Console.ReadLine();

    }

    private static async Task RegisterAndLogin()
    {
        await service.Register("test@test.de", "Pa$$w0rd");
        await service.Login("test@test.de", "Pa$$w0rd");
    }

    private static async Task<bool> TryLoadMovie()
    {
        try
        {
            var movie = await service.GetMovie(33);
            Console.WriteLine(movie.Title);
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
            return false;
        }
    }
}
