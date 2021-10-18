using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.StoreUserAgg
{
    public class StoreUserVM
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public long StoreRoleId { get; set; }
        public string StoreRoleName { get; set; }
        public string StoreName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public string CreationDate { get; set; }
    }

    public class RegisterStoreUserVM
    {
        public long StoreId { get; set; }

        [Display(Name = "نقش")]
        public long StoreRoleId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Password { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string City { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Province { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Address { get; set; }
    }

    public class EditStoreUserVM
    {
        public long Id { get; set; }

        public long StoreId { get; set; }

        [Display(Name = "نقش")]
        public long StoreRoleId { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string LastName { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        [MaxLength(200, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Password { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string City { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Province { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Address { get; set; }
    }

    public class LoginStoreUserVM
    {
        [Display(Name = "کد فروشگاه")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(8, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string StoreCode { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(200, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        public string Password { get; set; }
    }

    public class AddressStoreUserVM
    {
        public long Id { get; set; }

        [Display(Name = "شهر")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string City { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Province { get; set; }

        [Display(Name = "آدرس")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Address { get; set; }
    }
}
