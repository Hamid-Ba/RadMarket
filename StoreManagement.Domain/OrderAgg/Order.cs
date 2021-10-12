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
        public string MobileNumber { get; set; }
        public long RefId { get; private set; }
        public bool IsPayed { get; private set; }
        public string IssueTracking { get; private set; }
        public DateTime? PlaceOrderDate { get; private set; }
        
        public PaymentMethodType PaymentMethod { get; private set; }

        public List<OrderItem> OrderItems { get; private set; }

        public Order(long userId)
        {
            UserId = userId;
            OrderItems = new List<OrderItem>();
        }

        public Order(long userId, double totalPrice, double discountPrice, double payAmount, string mobileNumber, PaymentMethodType paymentMethod)
        {
            UserId = userId;
            TotalPrice = totalPrice;
            DiscountPrice = discountPrice;
            PayAmount = payAmount;
            MobileNumber = mobileNumber;
            IsPayed = false;
            PaymentMethod = paymentMethod;
        }

        public string PaymentSuccedded(PaymentMethodType type)
        {
            if (type == PaymentMethodType.PayOffline)
                IssueTracking = "Off" + Guid.NewGuid().ToString().Substring(0, 7);
            else
                IssueTracking = "On" + Guid.NewGuid().ToString().Substring(0, 7);

            IsPayed = true;
            PlaceOrderDate = DateTime.Now;
            PaymentMethod = type;

            return IssueTracking;
        }

        public void FillInfo(double totalPrice ,double discountPrice,double payAmount,string mobile)
        {
            TotalPrice = totalPrice;
            DiscountPrice = discountPrice;
            PayAmount = payAmount;
            MobileNumber = mobile;
        }

        public void ChoosePaymentType(PaymentMethodType type) => PaymentMethod = type;

        public void PaymentSuccedded(long refId)
        {
            if (refId > 0) RefId = refId;
            IsPayed = true;
            PlaceOrderDate = DateTime.Now;
        }

        //public void SetOrderStatus(OrderStatus status)
        //{
        //    Status = status;
        //    LastUpdateDate = System.DateTime.Now;
        //}

    }
}
