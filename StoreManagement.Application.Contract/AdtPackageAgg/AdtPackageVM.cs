using Framework.Application;
using Framework.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Application.Contract.AdtPackageAgg
{
    public class AdtPackageVM
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public AdtType Type { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public long OrderCount { get; set; }
    }

    public class CreateAdtPackageVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, 1, ErrorMessage = "حداقل مقدار {1} و حداکثر مقدار {2} می باشد")]
        public AdtType Type { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, Double.MaxValue, ErrorMessage = "حداقل مقدار {1} و حداکثر مقدار {2} می باشد")]
        public double Cost { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }
    }

    public class EditAdtPackageVM : CreateAdtPackageVM
    {
        public long Id { get; set; }
        public string ImageName { get; set; }
    }
}
