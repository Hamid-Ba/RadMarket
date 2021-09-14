using AccountManagement.Domain.StoreRolePermissionAgg;
using AccountManagement.Domain.StoreUserAgg;
using Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.StoreRoleAgg
{
    public class StoreRole : EntityBase
    {
        public long StoreId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public List<StoreUser> Users { get; private set; }
        public List<StoreRolePermission> Permissions { get; private set; }

        public StoreRole(long storeId, string name, string description)
        {
            StoreId = storeId;
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
