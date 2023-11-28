namespace MovieBase.Common.Interfaces;
public interface IMovieService
{
    Task<IEnumerable<Movie>> GetPageAsync(int pageSize, int pageNo);
}
