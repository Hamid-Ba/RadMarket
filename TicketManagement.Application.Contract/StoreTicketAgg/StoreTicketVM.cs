using Framework.Application;
using System;
using System.ComponentModel.DataAnnotations;

namespace TicketManagement.Application.Contract.StoreTicketAgg
{
    public class StoreTicketVM
    {
        public long Id { get; set; }
        public string Title { get;  set; }
        public long FirstStoreId { get; set; }
        public long SecondStoreId { get; set; }
        public string CreationDate { get; set; }
    }

    public class CreateStoreTicketVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Title { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public long FirstStoreId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1,long.MaxValue,ErrorMessage = ValidationMessage.IsRequired)]
        public long SecondStoreId { get; set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Message { get; set; }
    }

    public class AddStoreTicketMessageVM
    {
        public long StoreTicketId { get;  set; }
        public long SenderId { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessage.IsRequired)]
        public long ReciverId { get;  set; }

        [Display(Name = "پیام")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Message { get;  set; }
    }
}
