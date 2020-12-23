using Frapper.Entities.UserMaster;

namespace Frapper.Repository.UserMaster.Command
{
    public interface IUserTokensCommand
    {
        void Add(UserTokens userTokens);
    }
}