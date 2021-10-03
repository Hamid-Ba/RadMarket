using RadMarket.Query.Contracts.CategoryAgg;

namespace RadMarket.Query.Contracts.ProductAgg
{
    public class ProductQueryVM
    {
        public long Id { get; set; }
        public long StoreId { get;  set; }
        public string StoreName { get;  set; }
        public long CategoryId { get;  set; }
        public string CategoryName { get;  set; }
        public string Code { get;  set; }
        public long OrderCount { get;  set; }
        public string Name { get;  set; }
        public string Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public int EachBoxCount { get;  set; }
        public double ConsumerPrice { get;  set; }
        public double PurchasePrice { get;  set; }
        public double MoneyGain { get;  set; }
        public int Stock { get;  set; }
        public int Prize { get;  set; }
        public string Description { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
        public int? DiscountRate { get; set; }
        public bool HasAdtCharge { get; set; }

        public CategoryQueryVM Category { get; set; }
    }
}
