using System.Threading.Tasks;
using AboutMyMovie.Models;

namespace AboutMyMovie.Interfaces
{
    public interface IMovieApi
    {
        Task<Movie> GetMovieInfoAsync(string name);
    }
}
