namespace StoreManagement.Application.Contract.VisitorAgg
{
    public class VisitorVM
    {
        public long Id { get; set; }
        public long UserCount { get; set; }
        public string FullName { get; set; }
        public string UniqueCode { get; set; }
        public string Mobile { get; set; }
    }

    public class CreateVisitorVM
    {
        public string FullName { get; set; }
        public string Mobile { get; set; }
    }

    public class EditVisitorVM : CreateVisitorVM
    {
        public long Id { get; set; }
    }
}
