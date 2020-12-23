using Microsoft.EntityFrameworkCore;

namespace Frapper.Repository.Movies.Command
{
    public class MoviesCommand : IMoviesCommand
    {
        private readonly FrapperDbContext _frapperDbContext;
        public MoviesCommand(FrapperDbContext frapperDbContext)
        {
            _frapperDbContext = frapperDbContext;
        }
        public void Add(Entities.Movies.Movies movies)
        {
            _frapperDbContext.Movies.Add(movies);
        }

        public void Update(Entities.Movies.Movies movies)
        {
            _frapperDbContext.Entry(movies).State = EntityState.Modified;
        }

        public void Delete(Entities.Movies.Movies movies)
        {
            _frapperDbContext.Entry(movies).State = EntityState.Deleted;
        }
    }
}