using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Application.Contract.CategoryAgg
{
    public class CategoryVM
    {
        public long Id { get;  set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string KeyWords { get;  set; }
        public string MetaDescription { get;  set; }
        public string Slug { get;  set; }
    }

    public class CreateCategoryVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string KeyWords { get; set; }

        [Display(Name = "توضیحات متا")]
        public string MetaDescription { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }
    }

    public class EditCategoryVM : CreateCategoryVM
    {
        public long Id { get; set; }
    }
}
