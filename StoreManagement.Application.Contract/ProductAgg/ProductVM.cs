using Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Application.Contract.ProductAgg
{
    public class ProductVM
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public int EachBoxCount { get; set; }
        public double ConsumerPrice { get; set; }
        public double PurchasePrice { get; set; }
        public double MoneyGain { get; set; }
        public int Stock { get; set; }
        public int Prize { get; set; }
        public string Description { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string CreationDate { get; set; }
        public string ProductAcceptOrRejectDescription { get; set; }
        public ProductAcceptanceState ProductAcceptanceState { get; set; }
    }

    public class CreateProductVM
    {
        public long StoreId { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public IFormFile Picture { get; set; }

        [Display(Name = "جایگزین تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Display(Name = "تعداد در هر بسته")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int EachBoxCount { get; set; }

        [Display(Name = "قیمت مصرف کننده")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double ConsumerPrice { get; set; }

        [Display(Name = "قیمت خریدار")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double PurchacePrice { get; set; }

        [Display(Name = "تعداد در انبار")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Stock { get; set; }

        [Display(Name = "جایزه به ازای تعداد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Prize { get; set; }

        [Display(Name = "درباره محصول")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }
    }

    public class EditProductVM
    {
        public long Id { get; set; }

        public string PictureName { get; set; }

        public long StoreId { get; set; }

        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long CategoryId { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile Picture { get; set; }

        [Display(Name = "جایگزین تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PictureTitle { get; set; }

        [Display(Name = "تعداد در هر بسته")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int EachBoxCount { get; set; }

        [Display(Name = "قیمت مصرف کننده")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double ConsumerPrice { get; set; }

        [Display(Name = "قیمت خریدار")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, double.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public double PurchacePrice { get; set; }

        [Display(Name = "تعداد در انبار")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Stock { get; set; }

        [Display(Name = "جایزه به ازای تعداد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Prize { get; set; }

        [Display(Name = "درباره محصول")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get; set; }

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get; set; }

        [Display(Name = "توضیحات متا")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get; set; }
    }

    public class ChangeStatusProductVM
    {
        public long Id { get; set; }
        public ProductAcceptanceState State { get; set; }
        public string Description { get; set; }
    }

    public class SearchStoreVM
    {
        public string Code { get; set; }
    }
}