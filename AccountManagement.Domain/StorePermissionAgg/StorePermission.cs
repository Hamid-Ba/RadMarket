using AccountManagement.Domain.StoreRolePermissionAgg;
using System.Collections.Generic;

namespace AccountManagement.Domain.StorePermissionAgg
{
    public class StorePermission
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public long? ParentId { get; private set; }

        public List<StoreRolePermission> Roles { get; private set; }
    }
}
