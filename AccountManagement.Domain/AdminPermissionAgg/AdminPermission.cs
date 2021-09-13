namespace AccountManagement.Domain.AdminPermissionAgg
{
    public class AdminPermission
    {
        public long Id { get; private set; }
        public string Title { get; private set; }
        public long? ParentId { get; private set; }
    }
}
