using Framework.Domain;
using System;

namespace AdminManagement.Domain.BannerAgg
{
    public class Banner : EntityBase
    {
        public long StoreId { get; private set; }
        public string Picture { get; private set; }
        public string PictuerAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string ColSize { get; private set; }
        public string Url { get; private set; }
        public BannerPosition Position { get; private set; }
        public bool IsActive { get; private set; }

        public Banner(long storeId,string picture, string pictuerAlt, string pictureTitle, string colSize, string url, BannerPosition position)
        {
            StoreId = storeId;
            Picture = picture;
            PictuerAlt = pictuerAlt;
            PictureTitle = pictureTitle;
            ColSize = colSize;
            Url = url;
            Position = position;
            IsActive = false;
        }

        public void Edit(long storeId,string picture, string pictureAlt, string pictureTitle, string colSize, string url, BannerPosition position)
        {
            StoreId = storeId;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictuerAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ColSize = colSize;
            Url = url;
            Position = position;
            LastUpdateDate = DateTime.Now;
        }

        public void ActiveOrDeActive(bool which)
        {
            IsActive = which;
            LastUpdateDate = DateTime.Now;
        }
    }
}