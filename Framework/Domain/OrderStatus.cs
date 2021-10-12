using System.ComponentModel.DataAnnotations;

namespace Framework.Domain
{
    public enum OrderStatus
    {
        [Display(Name = "سفارش ایجاد گشت")]
        OrderCreated,

        [Display(Name = "تحویل مامور پست داده شد")]
        AgentRecived,
    }
}
