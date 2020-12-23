using Frapper.ViewModel.UserMaster;
using System.Linq;

namespace Frapper.Repository.UserMaster.Queries
{
    public class UserMasterQueries : IUserMasterQueries
    {
        private readonly FrapperDbContext _frapperDbContext;
        public UserMasterQueries(FrapperDbContext frapperDbContext)
        {
            _frapperDbContext = frapperDbContext;
        }

        public bool CheckUserExists(string username)
        {
            var userdata = (from tempuser in _frapperDbContext.UserMasters
                            where tempuser.UserName == username
                            select tempuser).Any();

            return userdata;
        }
        public bool CheckEmailExists(string emailid)
        {
            var userdata = (from tempuser in _frapperDbContext.UserMasters
                            where tempuser.EmailId == emailid
                            select tempuser).Any();

            return userdata;
        }
        public bool CheckMobileNoExists(string mobileno)
        {
            var userdata = (from tempuser in _frapperDbContext.UserMasters
                            where tempuser.MobileNo == mobileno
                            select tempuser).Any();

            return userdata;
        }

        public CommonUserDetailsViewModel GetCommonUserDetailsbyUserName(string username)
        {
            var userdata = (from tempuser in _frapperDbContext.UserMasters
                            join assignedRoles in _frapperDbContext.AssignedRoles on tempuser.UserId equals assignedRoles.UserId
                            join roleMaster in _frapperDbContext.RoleMasters on assignedRoles.RoleId equals roleMaster.RoleId
                            where tempuser.UserName == username
                            select new CommonUserDetailsViewModel()
                            {
                                FirstName = tempuser.FirstName,
                                EmailId = tempuser.EmailId,
                                LastName = tempuser.LastName,
                                RoleId = roleMaster.RoleId,
                                UserId = tempuser.UserId,
                                RoleName = roleMaster.RoleName,
                                Status = tempuser.Status,
                                UserName = tempuser.UserName,
                                PasswordHash = tempuser.PasswordHash,
                                MobileNo = tempuser.MobileNo
                            }).FirstOrDefault();

            return userdata;
        }

        public CommonUserDetailsViewModel GetCommonUserDetailsbyUserId(long userId)
        {
            var userdata = (from tempuser in _frapperDbContext.UserMasters
                            join assignedRoles in _frapperDbContext.AssignedRoles on tempuser.UserId equals assignedRoles.UserId
                            join roleMaster in _frapperDbContext.RoleMasters on assignedRoles.RoleId equals roleMaster.RoleId
                            where tempuser.UserId == userId
                            select new CommonUserDetailsViewModel()
                            {
                                FirstName = tempuser.FirstName,
                                EmailId = tempuser.EmailId,
                                LastName = tempuser.LastName,
                                RoleId = roleMaster.RoleId,
                                UserId = tempuser.UserId,
                                RoleName = roleMaster.RoleName,
                                Status = tempuser.Status,
                                UserName = tempuser.UserName,
                                PasswordHash = tempuser.PasswordHash,
                                MobileNo = tempuser.MobileNo
                            }).FirstOrDefault();

            return userdata;
        }
    }
}