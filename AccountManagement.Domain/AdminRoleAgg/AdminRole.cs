using AccountManagement.Domain.AdminRolePermissionAgg;
using AccountManagement.Domain.AdminUserAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AdminRoleAgg
{
    public class AdminRole : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<AdminUser> Users { get; private set; }
        public List<AdminRolePermission> Permissions { get; private set; }

        public AdminRole(string name, string description)
        {
            Name = name;
            Description = description;
        }

        public void Edit(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
