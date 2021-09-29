namespace RadMarket.Query.Contracts.CategoryAgg
{
    public class CategoryQueryVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}