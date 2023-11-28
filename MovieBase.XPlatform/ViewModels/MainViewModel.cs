using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MovieBase.Common;
using MovieBase.Common.Interfaces;
using System.Collections.ObjectModel;

namespace MovieBase.XPlatform.ViewModels;
public partial class MainViewModel : BaseViewModel
{
    public MainViewModel(IMovieService movieService, INavigationService navigation)
    {
        _movieService = movieService;
        _navigation = navigation;
        LoadMovies();
    }

    private async void LoadMovies()
    {
        Movies = new ObservableCollection<Movie>(await _movieService.GetPageAsync(20, 0));
        Title = $"Loaded {Movies.Count} Movies";
    }

    [RelayCommand]
    private async Task Navigate()
    {
        await _navigation.NavigateAsync("Details", new Dictionary<string, object> { { "Title", "Called from Main" } });
    }

    [ObservableProperty]
    private ICollection<Movie>? _movies;

    private readonly IMovieService _movieService;
    private readonly INavigationService _navigation;
}
