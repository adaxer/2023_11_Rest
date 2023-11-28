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
        _movieService = movieService;
    }

    private async void LoadMovies()
    {
        Movies = new ObservableCollection<Movie>(await _movieService.GetPageAsync(20, 0));
        Title = $"Loaded {Movies.Count} Movies";
    }

    [ObservableProperty]
    private string _title  = "Hello Maui";

    [ObservableProperty]
    private ICollection<Movie>? _movies;
    private readonly IMovieService _movieService;
}
