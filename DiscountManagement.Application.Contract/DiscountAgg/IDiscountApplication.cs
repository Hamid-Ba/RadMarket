using Framework.Application;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contract.DiscountAgg
{
    public interface IDiscountApplication
    {
        Task<OperationResult> Delete(long id);
        Task<OperationResult> Edit(EditDiscountVM command);
        Task<OperationResult> Create(CreateDiscountVM command);
        Task<EditDiscountVM> GetDetailForEditBy(long id,long storeId);
    }
}
