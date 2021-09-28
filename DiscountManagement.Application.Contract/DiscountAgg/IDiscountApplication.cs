using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contract.DiscountAgg
{
    public interface IDiscountApplication
    {
        Task<OperationResult> Delete(long id);
        Task<OperationResult> Edit(EditDiscountVM command);
        Task<IEnumerable<DiscountVM>> GetAllBy(long storeId);
        Task<OperationResult> Create(CreateDiscountVM command);
        Task<EditDiscountVM> GetDetailForEditBy(long id,long storeId);
    }
}
