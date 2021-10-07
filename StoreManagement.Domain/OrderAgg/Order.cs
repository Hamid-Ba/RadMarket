using Framework.Domain;
using System.Collections.Generic;

namespace StoreManagement.Domain.OrderAgg
{
    public class Order : EntityBase
    {
        public long UserId { get; private set; }
        public double TotalPrice { get; private set; }
        public double DiscountPrice { get; private set; }
        public double PayAmount { get; private set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public long RefId { get; private set; }
        public bool IsPayed { get; private set; }
        public OrderStatus Status { get; private set; }
        public PaymentMethodType PaymentMethod { get; private set; }

        public List<OrderItem> OrderItems { get; private set; }

        public Order(long userId) => UserId = userId;

        public Order(long userId, double totalPrice, double discountPrice, double payAmount, string address, string mobileNumber, PaymentMethodType paymentMethod)
        {
            UserId = userId;
            TotalPrice = totalPrice;
            DiscountPrice = discountPrice;
            PayAmount = payAmount;
            Address = address;
            MobileNumber = mobileNumber;
            IsPayed = false;
            Status = OrderStatus.OrderCreated;
            PaymentMethod = paymentMethod;
            OrderItems = new List<OrderItem>();
        }

        public void PaymentSuccedded(long refId)
        {
            if (refId > 0) RefId = refId;
            IsPayed = true;
        }

        public void SetOrderStatus(OrderStatus status)
        {
            Status = status;
            LastUpdateDate = System.DateTime.Now;
        }

        public void AddItem(OrderItem item) => OrderItems.Add(item);
    }
}
