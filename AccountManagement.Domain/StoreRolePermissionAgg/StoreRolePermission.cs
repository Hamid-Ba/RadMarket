using AccountManagement.Domain.StorePermissionAgg;
using AccountManagement.Domain.StoreRoleAgg;


namespace AccountManagement.Domain.StoreRolePermissionAgg
{
    public class StoreRolePermission
    {
        public long StoreRoleId { get; private set; }
        public long StorePermissionId { get; private set; }

        public StoreRole StoreRole { get; private set; }
        public StorePermission StorePermission { get; private set; }

        public StoreRolePermission(long storeRoleId, long storePermissionId)
        {
            StoreRoleId = storeRoleId;
            StorePermissionId = storePermissionId;
        }
    }
}
