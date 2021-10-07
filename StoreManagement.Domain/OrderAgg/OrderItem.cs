using Framework.Domain;
using StoreManagement.Domain.ProductAgg;

namespace StoreManagement.Domain.OrderAgg
{
    public class OrderItem : EntityBase
    {
        public long OrderId { get; private set; }
        public long ProductId { get; private set; }
        public double UnitPrice { get; private set; }
        public int DiscountRate { get; private set; }
        public int Count { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        public OrderItem(long productId, double unitPrice, int discountRate, int count)
        {
            ProductId = productId;
            UnitPrice = unitPrice;
            DiscountRate = discountRate;
            Count = count;
        }
    }
}