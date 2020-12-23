using System.Linq;
using System.Threading.Tasks;
using Frapper.Common;

namespace Frapper.Repository.Movies.Queries
{
    public interface IMoviesQueries
    {
        Entities.Movies.Movies GetMoviesbyId(int? moviesId);
        IQueryable<Entities.Movies.Movies> GetMovies();
    }
}