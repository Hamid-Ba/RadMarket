using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Framework.Application.TicketComponents;

namespace TicketManagement.Application.Contract.TicketAgg
{
    public class CreateTicketVM
    {
        public long UserId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(150, ErrorMessage = "حداکثر کاراکتر {1} می باشد")]
        public string Title { get; set; }
        public TicketSection Section { get; set; }
        public TicketNecessary Necessary { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Text { get; set; }
    }

    public class AddMessageTicketVM
    {
        public long TicketId { get; set; }
        public long UserId { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Text { get; set; }
    }
}