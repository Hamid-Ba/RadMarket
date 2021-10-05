using Framework.Application;
using System;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contract.DiscountCodeAgg
{
    public class DiscountCodeVM
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public int Rate { get; set; }
        public string StartDate { get; set; }
        public DateTime GeorgianStartDate { get; set; }
        public string EndDate { get; set; }
        public DateTime GeorgianEndDate { get; set; }
        public int Count { get; set; }
        public string Reason { get; set; }
    }

    public class CreateDiscountCodeVM
    {
        [Display(Name = "کد")]
        [MaxLength(7,ErrorMessage = "حداکثر کاراکتر {1} می باشد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Code { get; set; }

        [Display(Name = "درصد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1,100,ErrorMessage = ValidationMessage.IsRequired)]
        public int Rate { get; set; }

        [Display(Name = "تاریخ شروع")]
        [MinLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [MaxLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string StartDate { get; set; }

        [Display(Name = "تاریخ پایان")]
        [MinLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [MaxLength(10, ErrorMessage = "حداقل کاراکتر مجاز {1} می باشد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string EndDate { get; set; }

        [Display(Name = "تعداد")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public int Count { get; set; }

        public string Reason { get; set; }
    }

    public class EditDiscountCodeVM : CreateDiscountCodeVM
    {
        public long Id { get; set; }
    }
}