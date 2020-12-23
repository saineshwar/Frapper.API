using Frapper.Entities.UserMaster;

namespace Frapper.Repository.UserMaster.Command
{
    public class UserTokensCommand : IUserTokensCommand
    {
        private readonly FrapperDbContext _frapperDbContext;
        public UserTokensCommand(FrapperDbContext frapperDbContext)
        {
            _frapperDbContext = frapperDbContext;
        }

        public void Add(UserTokens userTokens)
        {
            _frapperDbContext.UserTokens.Add(userTokens);
        }
    }
}