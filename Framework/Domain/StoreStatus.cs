using System.ComponentModel.DataAnnotations;

namespace Framework.Domain
{
    public enum StoreStatus
    {
        [Display(Name = "در حال بررسی")]
        UnderProgressed,

        [Display(Name = "تایید شده")]
        Confirmed,

        [Display(Name = "رد شده")]
        Rejected

    }
}
