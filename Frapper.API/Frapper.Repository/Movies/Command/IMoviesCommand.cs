namespace Frapper.Repository.Movies.Command
{
    public interface IMoviesCommand
    {
        void Add(Entities.Movies.Movies movies);
        void Update(Entities.Movies.Movies movies);
        void Delete(Entities.Movies.Movies movies);
    }
}