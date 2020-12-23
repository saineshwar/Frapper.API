using Frapper.Entities.UserMaster;
using Microsoft.EntityFrameworkCore;

namespace Frapper.Repository.UserMaster.Command
{
    public class AssignedRolesCommand : IAssignedRolesCommand
    {
        private readonly FrapperDbContext _frapperDbContext;
        public AssignedRolesCommand(FrapperDbContext frapperDbContext)
        {
            _frapperDbContext = frapperDbContext;
        }
        public void Add(AssignedRoles assignedRoles)
        {
            _frapperDbContext.AssignedRoles.Add(assignedRoles);
        }

        public void Update(AssignedRoles assignedRoles)
        {
            _frapperDbContext.Entry(assignedRoles).State = EntityState.Modified;
        }
    }
}