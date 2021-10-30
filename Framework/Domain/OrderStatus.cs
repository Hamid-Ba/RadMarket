using System.ComponentModel.DataAnnotations;

namespace Framework.Domain
{
    public enum OrderStatus
    {
        [Display(Name = "سفارش ایجاد گشت")]
        OrderCreated,

        [Display(Name = "در حال آماده سازی سفارش")]
        Prepreing,

        [Display(Name = "در حال تحویل")]
        Delivering,

        [Display(Name = "تحویل داده شد")]
        Deliverd
    }
}
