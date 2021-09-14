namespace AccountManagement.Application.Contract.StorePermissionAgg
{
    public class StorePermissionVM
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }
    }
}
