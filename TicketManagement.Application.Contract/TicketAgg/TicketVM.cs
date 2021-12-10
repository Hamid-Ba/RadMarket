using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Framework.Application;
using Framework.Application.TicketComponents;

namespace TicketManagement.Application.Contract.TicketAgg
{
    public class TicketVM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string StoreName { get; set; }
        public string Title { get; set; }
        public TicketSection Section { get; set; }
        public TicketStatus Status { get; set; }
        public TicketNecessary Necessary { get; set; }
        public bool IsReadByOwner { get; set; }
        public bool IsReadByAdmin { get; set; }
        public string CreationDate { get; set; }
        public IEnumerable<MessagesVM> Messages { get; set; }
    }

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
        public long SenderId { get; set; }
        public long ReciverId { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Text { get; set; }
    }

    public class MessagesVM
    {
        public long Id { get;  set; }
        public long TicketId { get;  set; }
        public long SenderId { get; set; }
        public long ReciverId { get; set; }
        public string Text { get;  set; }
        public string CreationDate { get;  set; }
    }
}