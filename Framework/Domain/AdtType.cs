using System.ComponentModel.DataAnnotations;

namespace Framework.Domain
{
    public enum AdtType
    {
        [Display(Name = "بنر")]
        Banner,
        
        [Display(Name = "محصولات")]
        Product
    }
}
