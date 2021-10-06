using Framework.Infrastructure;

namespace StoreManagement.Application.Contract.PackageOrderAgg
{
    public class PackageOrderVM
    {
        public long Id { get; set; }
        public long StoreId { get;  set; }
        public string StoreCode { get; set; }
        public string StoreName { get; set; }
        public long PackageId { get;  set; }
        public string PackageName { get;  set; }
        public PackageType Type { get;  set; }
        public double TotalPrice { get;  set; }
        public double DiscountPrice { get;  set; }
        public double PayAmount { get;  set; }
        public string MobileNumber { get; set; }
        public long RefId { get;  set; }
        public bool IsPayed { get;  set; }
        public string CreationDate { get; set; }
    }

    public class CreatePackageOrderVM
    {
        public long StoreId { get; set; }
        public long PackageId { get; set; }
        public PackageType Type { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string MobileNumber { get; set; }
    }
}
