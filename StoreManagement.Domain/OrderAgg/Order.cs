using Framework.Domain;
using System;
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
        public string IssueTracking { get; private set; }
        public OrderStatus Status { get; private set; }
        public PaymentMethodType PaymentMethod { get; private set; }

        public List<OrderItem> OrderItems { get; private set; }

        public Order(long userId)
        {
            UserId = userId;
            OrderItems = new List<OrderItem>();
        }

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
        }

        public string PaymentSuccedded(PaymentMethodType type)
        {
            if (type == PaymentMethodType.PayOffline)
                IssueTracking = "Off" + Guid.NewGuid().ToString().Substring(0, 7);
            else
                IssueTracking = "On" + Guid.NewGuid().ToString().Substring(0, 7);

            IsPayed = true;

            return IssueTracking;
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

    }
}
