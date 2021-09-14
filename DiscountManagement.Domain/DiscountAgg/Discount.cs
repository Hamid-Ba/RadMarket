using Framework.Domain;
using System;

namespace DiscountManagement.Domain.DiscountAgg
{
    public class Discount : EntityBase
    {
        public long StoreId { get; private set; }
        public long ProductId { get; private set; }
        public int DiscountRate { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; private set; }

        public Discount(long storeId,long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            StoreId = storeId;
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;
        }

        public void Edit(long productId, int discountRate, DateTime startDate, DateTime endDate, string reason)
        {
            ProductId = productId;
            DiscountRate = discountRate;
            StartDate = startDate;
            EndDate = endDate;
            Reason = reason;

            LastUpdateDate = DateTime.Now;
        }
    }
}
