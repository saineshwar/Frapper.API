using Frapper.Entities.UserMaster;

namespace Frapper.Repository.UserMaster.Queries
{
    public interface IUserTokensQueries
    {
        UserTokens GetUserSaltbyUserid(long userId);
    }
}