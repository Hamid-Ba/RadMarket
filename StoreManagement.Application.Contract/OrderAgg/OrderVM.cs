using Framework.Domain;
using System.Collections.Generic;

namespace StoreManagement.Application.Contract.OrderAgg
{
    public class OrderVM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public long RefId { get; set; }
        public bool IsPayed { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
        public string CreationDate { get; set; }

        public List<OrderItemsVM> Items { get; set; }
    }

    public class CreateOrderVM
    {
        public long UserId { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string Address { get; set; }
        public string MobileNumber { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
    }

    public class OrderItemsVM
    {
        public long Id { get; set; }
        public long OrderId { get;  set; }
        public long ProductId { get;  set; }
        public double UnitPrice { get;  set; }
        public int DiscountRate { get;  set; }
        public int Count { get;  set; }
    }
}