using System.ComponentModel.DataAnnotations;

namespace Framework.Domain
{
    public enum PaymentMethodType
    {
        [Display(Name = "پرداخت آنلاین")]
        PayOnline,
        
        [Display(Name = "پرداخت فیزیکی")]
        PayOffline
    }
}
