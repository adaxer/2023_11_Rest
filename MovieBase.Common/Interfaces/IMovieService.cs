namespace MovieBase.Common.Interfaces;
public interface IMovieService
{
    Task<IEnumerable<Movie>> GetMoviePage(int pageSize, int pageNo);
}
