using Framework.Domain;
using Framework.Infrastructure;
using StoreManagement.Domain.StoreAgg;

namespace StoreManagement.Domain.PackageOrderAgg
{
    public class PackageOrder : EntityBase
    {
        public long StoreId { get; private set; }
        public long PackageId { get; private set; }
        public PackageType Type { get; private set; }
        public double TotalPrice { get; private set; }
        public double DiscountPrice { get; private set; }
        public double PayAmount { get; private set; }
        public string MobileNumber { get; set; }
        public long RefId { get; private set; }
        public bool IsPayed { get; private set; }

        public Store Store { get; private set; }

        public PackageOrder(long storeId,long packageId, PackageType type,double totalPrice,double discountPrice, double payAmount, string mobileNumber)
        {
            StoreId = storeId;
            PackageId = packageId;
            Type = type;
            TotalPrice = totalPrice;
            DiscountPrice = discountPrice;
            PayAmount = payAmount;
            MobileNumber = mobileNumber;
            IsPayed = false;
        }

        public void PaymentSucceeded(long refId)
        {
            if (refId > 0) RefId = refId;
            IsPayed = true;
        }
    }
}
