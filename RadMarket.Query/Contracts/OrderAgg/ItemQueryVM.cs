namespace RadMarket.Query.Contracts.OrderAgg
{
    public class ItemQueryVM
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductTitle { get; set; }
        public double UnitPrice { get; set; }
        public double DiscountPrice { get; set; }
        public double PayAmount { get; set; }
        public int DiscountRate { get; set; }
        public int Count { get; set; }
    }
}
