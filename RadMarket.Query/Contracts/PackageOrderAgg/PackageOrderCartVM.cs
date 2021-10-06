using Framework.Infrastructure;

namespace RadMarket.Query.Contracts.PackageOrderAgg
{
    public class PackageOrderCartVM
    {
        public long PackageId { get; set; }
        public string PackageName { get; set; }
        public PackageType Type { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string DiscountCode { get; set; }

        public PackageOrderCartVM(long packageId, string packageName, PackageType type, double totalPrice, double payAmount)
        {
            PackageId = packageId;
            PackageName = packageName;
            Type = type;
            TotalPrice = totalPrice;
            PayAmount = payAmount;
        }

        public void CalculatePriceViaDiscount(string discountCode,int discountRate)
        {
            DiscountCode = discountCode;
            DiscountPrice = TotalPrice * ((double)discountRate / 100);
            PayAmount = TotalPrice - DiscountPrice;
        }
    }
}
