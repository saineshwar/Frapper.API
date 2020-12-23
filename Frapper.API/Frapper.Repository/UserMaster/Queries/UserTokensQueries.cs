using System.Linq;
using Frapper.Entities.UserMaster;

namespace Frapper.Repository.UserMaster.Queries
{
    public class UserTokensQueries : IUserTokensQueries
    {
        private readonly FrapperDbContext _frapperDbContext;
        public UserTokensQueries(FrapperDbContext frapperDbContext)
        {
            _frapperDbContext = frapperDbContext;
        }
        public UserTokens GetUserSaltbyUserid(long userId)
        {
            var usertoken = (from tempuser in _frapperDbContext.UserTokens
                where tempuser.UserId == userId
                select tempuser).FirstOrDefault();

            return usertoken;
        }
    }
}