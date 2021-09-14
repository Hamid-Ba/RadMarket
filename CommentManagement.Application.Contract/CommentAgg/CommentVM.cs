using System.ComponentModel.DataAnnotations;
using Framework.Application;

namespace CommentManagement.Application.Contract.CommentAgg
{
    public class CommentVM
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public int Type { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }

        public long ParentId { get; set; }
    }

    public class CreateCommentVM
    {
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int Type { get; set; }
        public long ParentId { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر هست !")]
        public string Email { get; set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Message { get; set; }
    }

    public class SearchCommentVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
