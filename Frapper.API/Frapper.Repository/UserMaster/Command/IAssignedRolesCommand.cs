using Frapper.Entities.UserMaster;

namespace Frapper.Repository.UserMaster.Command
{
    public interface IAssignedRolesCommand
    {
        void Add(AssignedRoles assignedRoles);
        void Update(AssignedRoles assignedRoles);
    }
}