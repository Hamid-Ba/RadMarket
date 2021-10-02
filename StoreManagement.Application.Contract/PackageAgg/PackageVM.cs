using Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.PackageAgg
{
    public class PackageVM
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageName { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public long OrderCount { get; set; }
    }

    public class CreatePackageVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Display(Name = "تصویر")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "قیمت")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0, Double.MaxValue, ErrorMessage = "حداقل مقدار {1} و حداکثر مقدار {2} می باشد")]
        public double Cost { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Description { get; set; }

    }

    public class EditPackageVM : CreatePackageVM
    {
        public long Id { get; set; }
        public string ImageName { get; set; }
    }
}