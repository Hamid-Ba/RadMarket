using System;

namespace RadMarket.Query.Contracts.ArticleAgg
{
    public class ArticleQueryVM
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public long CategoryId { get;  set; }
        public string CategoryName { get;  set; }
        public string PictureName { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string PublishDate { get;  set; }
        public DateTime GeorgianPublishDate { get;  set; }
        public string Author { get;  set; }
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
    }
}
