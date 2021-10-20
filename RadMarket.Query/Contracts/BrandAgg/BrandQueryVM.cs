using System.Collections.Generic;

namespace RadMarket.Query.Contracts.BrandAgg
{
    public class BrandQueryVM
    {
        public long Id { get; set; }
        public long StoreId { get;  set; }
        public string Name { get;  set; }
        public List<ProductAgg.ProductQueryVM> Products { get; set; }
    }
}
