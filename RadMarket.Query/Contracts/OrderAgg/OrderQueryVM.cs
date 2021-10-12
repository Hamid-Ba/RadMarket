using Framework.Domain;
using System.Collections.Generic;

namespace RadMarket.Query.Contracts.OrderAgg
{
    public class OrderQueryVM
    {
        public long Id { get; set; }
        public double TotalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public string IssueTracking { get; set; }
        public string PlaceOrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentMethodType PaymentMethod{ get; set; }
        public List<ItemQueryVM> Items { get; set; }
    }
}
