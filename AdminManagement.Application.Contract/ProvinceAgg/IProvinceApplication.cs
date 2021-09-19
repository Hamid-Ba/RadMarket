using Framework.Application;
using System.Threading.Tasks;

namespace AdminManagement.Application.Contract.ProvinceAgg
{
    public interface IProvinceApplication
    {
        Task<OperationResult> Delete(long id);
        Task<EditProvinceVM> GetDetialForEditBy(long id);
        Task<OperationResult> Create(CreateProvinceVM command);
        Task<OperationResult> Edit(EditProvinceVM command);
    }
}
