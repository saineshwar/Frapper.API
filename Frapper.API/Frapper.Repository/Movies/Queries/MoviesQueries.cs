using System;
using System.Linq;
using System.Threading.Tasks;
using Frapper.Common;
using Microsoft.EntityFrameworkCore;

namespace Frapper.Repository.Movies.Queries
{
    public class MoviesQueries : IMoviesQueries
    {
        private readonly FrapperDbContext _frapperDbContext;
        public MoviesQueries(FrapperDbContext frapperDbContext)
        {
            _frapperDbContext = frapperDbContext;
        }

        public Entities.Movies.Movies GetMoviesbyId(int? moviesId)
        {
            var movie = _frapperDbContext.Movies
                .FirstOrDefault(m => m.MoviesID == moviesId);

            return movie;
        }


        public IQueryable<Entities.Movies.Movies> GetMovies()
        {
            var source = (from movies in _frapperDbContext.Movies.
                    OrderBy(a => a.MoviesID)
                          select movies).AsQueryable();

            return source;
        }
    }
}