using Framework.Domain;
using System;

namespace RadMarket.Query.Contracts.BannerAgg
{
    public class BannerQueryVM
    {
        public long Id { get; set; }
        public long StoreId { get;  set; }
        public string StoreName { get;  set; }
        public string Picture { get;  set; }
        public string PictuerAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string ColSize { get;  set; }
        public string Url { get;  set; }
        public BannerPosition Position { get;  set; }
        public bool IsActive { get;  set; }
        public DateTime CreationDate { get; set; }
    }
}
