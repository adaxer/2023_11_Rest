using CommunityToolkit.Mvvm.ComponentModel;
using MovieBase.Common;
using MovieBase.Common.Interfaces;
using System.Collections.ObjectModel;

namespace MovieBase.XPlatform.ViewModels;
public partial class MainViewModel : ObservableObject
{
    public MainViewModel(IMovieService movieService)
    {
        LoadMovies();
        this.movieService = movieService;
    }

    private async void LoadMovies()
    {
        Movies = new ObservableCollection<Movie>(await movieService.GetPageAsync(20, 0));
        Title = $"Loaded {Movies.Count} Movies";
    }

    [ObservableProperty]
    private string _title  = "Hello Maui";

    [ObservableProperty]
    private ICollection<Movie>? _movies;
    private readonly IMovieService movieService;
}
