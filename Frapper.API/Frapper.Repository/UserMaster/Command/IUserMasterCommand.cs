namespace Frapper.Repository.UserMaster.Command
{
    public interface IUserMasterCommand
    {
        void Add(Entities.UserMaster.UserMaster usermaster);
        void Update(Entities.UserMaster.UserMaster usermaster);
        void ChangeUserStatus(Entities.UserMaster.UserMaster usermaster);
        void UpdatePasswordandHistory(long userId, string passwordHash, string passwordSalt, string processType);
    }
}