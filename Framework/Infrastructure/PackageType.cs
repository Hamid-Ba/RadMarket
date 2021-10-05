using System.ComponentModel.DataAnnotations;

namespace Framework.Infrastructure
{
    public enum PackageType
    {
        [Display(Name = "فروشندگی")]
        Products,
        [Display(Name = "تبلیغاتی")]
        Adt
    }
}
