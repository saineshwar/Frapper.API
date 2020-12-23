using Frapper.Repository.Movies.Command;
using Frapper.Repository.UserMaster.Command;

namespace Frapper.Repository
{
    public interface IUnitOfWorkEntityFramework
    {
        IUserMasterCommand UserMasterCommand { get; }
        IAssignedRolesCommand AssignedRolesCommand { get; }
        IUserTokensCommand UserTokensCommand { get; }
        IMoviesCommand MoviesCommand { get; }
        bool Commit();
        void Dispose();
    }
}