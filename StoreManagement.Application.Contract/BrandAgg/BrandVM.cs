using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Application.Contract.BrandAgg
{
    public class BrandVM
    {
        public long Id { get; set; }
        public long StoreId { get; set; }
        public string Name { get; set; }
        public long ProductCount { get; set; }
    }

    public class CreateBrandVM
    {
        public long StoreId { get; set; }

        [Display(Name = "نام")]
        public string Name { get; set; }
    }

    public class EditBrandVM : CreateBrandVM
    {
        public long Id { get; set; }
    }
}
