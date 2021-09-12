using System.ComponentModel.DataAnnotations;

namespace Framework.Application
{
    public enum StoreStatus
    {
        [Display(Name = "تایید شده")]
        Confirmed,

        [Display(Name = "رد شده")]
        Rejected,

        [Display(Name = "در حال بررسی")]
        UnderProgressed
    }
}
