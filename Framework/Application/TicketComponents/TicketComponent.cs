using System.ComponentModel.DataAnnotations;

namespace Framework.Application.TicketComponents
{
    public enum TicketSection
    {
        [Display(Name = "پشتیبانی")]
        Support,
        
        [Display(Name = "فروش")]
        Sales,
        
        [Display(Name = "مدیریت")]
        Management,
        
        [Display(Name = "امنیت")]
        Security
    }

    public enum TicketStatus
    {
        [Display(Name = "دریافت شده")]
        Received,

        [Display(Name = "جواب داده شده")]
        Answered,

        [Display(Name = "بسته شده")]
        Closed
    }

    public enum TicketNecessary
    {
        [Display(Name = "کم")]
        Low,

        [Display(Name = "متوسط")]
        Medium,

        [Display(Name = "فوری")]
        High
    }
}