using Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contract.StoreRoleAgg
{
    public class StoreRoleVM
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
    }

    public class CreateStoreRoleVM
    {
        public long StoreId { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        [Display(Name = "دسترسی ها")]
        public long[] PermissionsId { get; set; }
    }

    public class EditStoreRoleVM : CreateStoreRoleVM
    {
        public long Id { get; set; }
    }
}
