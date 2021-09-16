using Framework.Application;
using Framework.Domain;
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Application.Contract.StoreAgg
{
    public class StoreVM
    {
        public long Id { get; set; }
        public long StoreAdminUserId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public StoreStatus Status { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string StoreGivenStatusReason { get; set; }
    }

    public class CreateStoreVM
    {
        public long StoreAdminUserId { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string MobileNumber { get; set; }
    }

    public class EditStoreVM
    {
        public long Id { get; set; }

        public long StoreAdminUserId { get; set; }

        [Display(Name = "نام فروشگاه")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(125, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Name { get; set; }

        [Display(Name = "شماره تلفن")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string PhoneNumber { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string MobileNumber { get; set; }

        [Display(Name = "درباره فروشگاه")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(1500, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Description { get; set; }

        [Display(Name = "آدرس فروشگاه")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(500, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Address { get; set; }
    }

    public class SpecifyStatusOfStoreVM
    {
        public long Id { get; set; }
        public StoreStatus Status { get; set; }

        [Display(Name = "دلیل")]
        public string StoreGivenStatusReason { get; set; }
    }
}