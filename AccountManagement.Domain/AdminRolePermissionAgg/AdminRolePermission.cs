using AccountManagement.Domain.AdminPermissionAgg;
using AccountManagement.Domain.AdminRoleAgg;

namespace AccountManagement.Domain.AdminRolePermissionAgg
{
    public class AdminRolePermission
    {
        public long AdminRoleId { get; private set; }
        public long AdminPermissionId { get; private set; }

        public AdminRole AdminRole { get; private set; }
        public AdminPermission AdminPermission { get; private set; }

        public AdminRolePermission(long adminRoleId, long adminPermissionId)
        {
            AdminRoleId = adminRoleId;
            AdminPermissionId = adminPermissionId;
        }
    }
}
