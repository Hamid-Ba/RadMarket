using DiscountManagement.Application.Contract.DiscountCodeAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountManagement.Domain.DiscountCodeAgg
{
    public interface IDiscountCodeRepository : IRepository<DiscountCode>
    {
        Task<IEnumerable<DiscountCodeVM>> GetAll();
        Task<EditDiscountCodeVM> GetDetailForEditBy(long id);
    }
}
