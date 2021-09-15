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
        public int Score{ get; set; }
        public int Type { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
    }

    public class CreateCommentVM
    {
        public long StoreId { get; set; }
        public long OwnerId { get; set; }
        public string OwnerName { get; set; }
        public int Type { get; set; }
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [MaxLength(11, ErrorMessage = "حداکثر تعداد کاراکتر مجاز {1} می باشد")]
        [MinLength(11, ErrorMessage = "حداقل تعداد کاراکتر مجاز {1} می باشد")]
        [RegularExpression("(0|\\+98)?([ ]|-|[()]){0,2}9[1|2|3|4]([ ]|-|[()]){0,2}(?:[0-9]([ ]|-|[()]){0,2}){8}", ErrorMessage = "لطفا شماره خود را به فرم صحیح وارد نمایید")]
        public string Mobile { get; set; }

        [Display(Name = "امتیاز")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        [Range(0,5,ErrorMessage = "لطفا امتیاز را وارد کنید")]
        public int Score { get; set; }
    }

    public class SearchCommentVM
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
    }
}
