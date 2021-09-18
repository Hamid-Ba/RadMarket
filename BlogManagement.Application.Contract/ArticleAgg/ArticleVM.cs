using System;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.ArticleAgg
{
    public class ArticleVM
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public long ArticleCategoryId { get;  set; }
        public string PictureName { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        public string PublishDate { get;  set; }
        public string Author { get; set; }
        public string ShortDescription { get;  set; }
        public string Description { get;  set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
    }

    public class CreateArticleVM
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = ValidationMessage.IsRequired)]
        public long ArticleCategoryId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public IFormFile Picture { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        public string PublishDate { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Author { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }
    }

    public class EditArticleVM : CreateArticleVM
    {
        public long Id { get; set; }
        public string PictureName { get; set; }
    }

    public class SearchArticleVM
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
    }
}