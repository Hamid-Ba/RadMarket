using Framework.Application;
using Framework.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace AdminManagement.Application.Contract.BannerAgg
{
    public class BannerVM
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string PictuerAlt { get; set; }
        public string PictureTitle { get; set; }
        public string ColSize { get; set; }
        public string Url { get; set; }
        public BannerPosition Position { get; set; }
    }

    public class CreateBannerVM
    {
        [Display(Name = "تصویر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public IFormFile Picture { get; set; }

        [Display(Name = "جایگزین تصویر")]
        public string PictuerAlt { get; set; }

        [Display(Name = "عنوان تصویر")]
        public string PictureTitle { get; set; }

        [Display(Name = "سایز")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string ColSize { get; set; }

        [Display(Name = "لینک")]
        public string Url { get; set; }

        [Display(Name = "موقعیت")]
        [Range(0, 2, ErrorMessage = ValidationMessage.IsRequired)]
        public BannerPosition Position { get; set; }
    }

    public class EditBannerVM : CreateBannerVM
    {
        public long Id { get; set; }
        public string PictureName { get; set; }
    }
}