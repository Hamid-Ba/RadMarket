using System;
using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace BlogManagement.Application.Contract.ArticleCategoryAgg
{
    public class ArticleCategoryVM
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public int ShowOrder { get;  set; }
        public int ArticleCount { get; set; }
        public string Slug { get;  set; }
        public string Keywords { get;  set; }
        public string MetaDescription { get;  set; }
    }

    public class CreateArticleCategoryVM
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(1,int.MaxValue,ErrorMessage = ValidationMessage.IsRequired)]
        public int ShowOrder { get; set; }
        
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }
    }

    public class EditArticleCategoryVM : CreateArticleCategoryVM
    {
        public long Id { get; set; }
    }
}