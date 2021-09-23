using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace AdminManagement.Application.Contract.ProvinceAgg
{
    public class ProvinceVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateProvinceVM
    {
        [Display(Name = "نام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(85,ErrorMessage = "حداکثر کاراکتر مجاز {1} می باشد")]
        public string Name { get; set; }
    }

    public class EditProvinceVM : CreateProvinceVM
    {
        public long Id { get; set; }
    }
}
