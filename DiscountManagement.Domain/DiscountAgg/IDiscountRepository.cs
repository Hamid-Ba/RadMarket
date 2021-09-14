using DiscountManagement.Application.Contract.DiscountAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace DiscountManagement.Domain.DiscountAgg
{
    public interface IDiscountRepository : IRepository<Discount>
    {
        Task<EditDiscountVM> GetDetailForEditBy(long id,long storeId);
    }
}
