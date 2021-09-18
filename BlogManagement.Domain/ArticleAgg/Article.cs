using System;
using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Domain;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : EntityBase
    {
        public string Title { get; private set; }
        public long ArticleCategoryId { get; private set; }
        public string PictureName { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public DateTime PublishDate { get; private set; }
        public string Author { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }

        public ArticleCategory ArticleCategory { get; private set; }

        public Article(string title, long articleCategoryId, string pictureName, string pictureAlt, string pictureTitle,
            DateTime publishDate, string author, string shortDescription, string description, string slug, string keywords, string metaDescription)
        {
            Title = title;
            ArticleCategoryId = articleCategoryId;
            PictureName = pictureName;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            PublishDate = publishDate;
            Author = author;
            ShortDescription = shortDescription;
            Description = description;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public void Edit(string title, long categoryId, string pictureName, string pictureAlt, string pictureTitle,
            DateTime publishDate, string author, string shortDescription, string description, string slug, string keywords, string metaDescription)
        {
            Title = title;
            ArticleCategoryId = categoryId;

            if (!string.IsNullOrWhiteSpace(pictureName))
                PictureName = pictureName;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            PublishDate = publishDate;
            Author = author;
            ShortDescription = shortDescription;
            Description = description;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;

            LastUpdateDate = DateTime.Now;
        }

    }
}
