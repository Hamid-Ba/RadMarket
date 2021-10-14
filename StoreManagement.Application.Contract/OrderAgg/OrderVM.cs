using Framework.Domain;
using System.Collections.Generic;

namespace StoreManagement.Application.Contract.OrderAgg
{
    public class OrderVM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserFullName { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string MobileNumber { get; set; }
        public long RefId { get; set; }
        public string IssueTrackingCode { get; set; }
        public bool IsPayed { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
        public string PalceOrderDate { get; set; }

        public List<OrderItemsVM> Items { get; set; }
    }

    public class PlaceOrderVM
    {
        public long UserId { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string MobileNumber { get; set; }
        public PaymentMethodType PaymentMethod { get; set; }
    }

    public class PlaceItemVM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public double PayAmount { get; set; }
        public double DiscountPrice { get; set; }
        public int Count { get; set; }
    }

    public class OrderItemsVM
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long StoreId { get; set; }
        public long ProductId { get; set; }
        public string StoreName { get; set; }
        public string StoreCode { get; set; }
        public string ProductName { get; set; }
        public double PayAmount { get; set; }
        public double DiscountPrice { get; set; }
        public double TotalPayAmount { get; set; }
        public int Count { get; set; }
        public OrderStatus Status { get; set; }
    }

    public class AddOrderItemsVM
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public int Count { get; set; }
    }

    public class ChangeOrderStatusVM
    {
        public long ItemId { get; set; }
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public string IssueTracking { get; set; }
        public OrderStatus Status { get; set; }
    }
}