using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contract.DiscountCodeAgg
{
    public interface IDiscountCodeApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<DiscountCodeVM>> GetAll();
        Task<EditDiscountCodeVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditDiscountCodeVM command);
        Task<OperationResult> Create(CreateDiscountCodeVM command);
    }
}
