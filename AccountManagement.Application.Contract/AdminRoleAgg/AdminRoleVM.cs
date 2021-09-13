using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.AdminRoleAgg
{
    public class AdminRoleVM
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Description { get;  set; }
    }

    public class CreateAdminRoleVM
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }
    }

    public class EditAdminRoleVM : CreateAdminRoleVM
    {
        public long Id { get; set; }
    }
}
