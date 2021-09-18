using System.ComponentModel.DataAnnotations;

namespace Framework.Application
{
    public enum ProductAcceptanceState
    {
        [Display(Name = "در حال بررسی")]
        UnderProgress,
        [Display(Name = "تایید شده")]
        Accepted,
        [Display(Name = "رد شده")]
        Rejected,
        [Display(Name = "حذف شده")]
        Deleted
    }
}
