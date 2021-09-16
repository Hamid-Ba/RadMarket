using Framework.Domain;
using System;

namespace AdminManagement.Domain.BannerAgg
{
    public class Banner : EntityBase
    {
        public string Picture { get; private set; }
        public string PictuerAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string ColSize { get; private set; }
        public string Url { get; private set; }
        public BannerPosition Position { get; private set; }

        public Banner(string picture,string pictureAlt,string pictureTitle, string colSize, string url, BannerPosition position)
        {
            Picture = picture;
            PictuerAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ColSize = colSize;
            Url = url;
            Position = position;
        }

        public void Edit(string picture, string pictureAlt, string pictureTitle, string colSize, string url, BannerPosition position)
        {
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictuerAlt = pictureAlt;
            PictureTitle = pictureTitle;
            ColSize = colSize;
            Url = url;
            Position = position;
            LastUpdateDate = DateTime.Now;
        }
    }
}
