namespace AccountManagement.Application.Contract.AdminPermissionAgg
{
    public class AdminPermissionVM
    {
        public long Id { get;  set; }
        public string Title { get;  set; }
        public long? ParentId { get;  set; }
    }
}
