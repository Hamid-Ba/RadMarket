namespace RadMarket.Query.Contracts.OrderAgg
{
    public class ItemQueryVM
    {
        public long UserId { get; set; }
        public long ProductId { get; set; }
        public double UnitPrice { get; set; }
        public int DiscountRate { get; set; }
        public int Count { get; set; }
    }
}
