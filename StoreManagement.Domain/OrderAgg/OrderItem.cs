using Framework.Domain;
using StoreManagement.Domain.ProductAgg;
using System;

namespace StoreManagement.Domain.OrderAgg
{
    public class OrderItem : EntityBase
    {
        public long OrderId { get; private set; }
        public long ProductId { get; private set; }
        public double PayAmount { get; private set; }
        public double DiscountPrice { get; private set; }
        public int Count { get; private set; }
        public OrderStatus Status { get; private set; }
        public bool IsPayedWithSite { get; private set; }
        public int SiteProfitPercentage { get; private set; }
        public DateTime DateOfPayedToSite { get; private set; }

        public Order Order { get; private set; }
        public Product Product { get; private set; }

        public OrderItem(long orderId,long productId,int count)
        {
            OrderId = orderId;
            ProductId = productId;
            Count = count;
            IsPayedWithSite = false;
        }

        public OrderItem(long orderId,long productId, double payAmount, double discountPrice, int count)
        {
            OrderId = orderId;
            ProductId = productId;
            PayAmount = payAmount;
            DiscountPrice = discountPrice;
            Count = count;
        }

        public int AddCount(int count) => Count += count;

        public void FillInfo(double discountPrice, double payAmount)
        {
            DiscountPrice = discountPrice;
            PayAmount = payAmount;

            var totalAmount = payAmount * Count;

            if (totalAmount >= SiteProfitPercentages.OneMillion && totalAmount < SiteProfitPercentages.TenMillion) SiteProfitPercentage = 10;
            else if (totalAmount >= SiteProfitPercentages.TenMillion) SiteProfitPercentage = 15;
            else SiteProfitPercentage = 5;
        }

        public void SetOrderStatus(OrderStatus status)
        {
            Status = status;
            LastUpdateDate = System.DateTime.Now;
        }

        public void PayToSite()
        {
            IsPayedWithSite = true;
            DateOfPayedToSite = DateTime.Now;
        }
    }
}