namespace AdminManagement.Application.Contract.ProvinceAgg
{
    public class ProvinceVM
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateProvinceVM
    {
        public string Name { get; set; }
    }

    public class EditProvinceVM : CreateProvinceVM
    {
        public long Id { get; set; }
    }
}
