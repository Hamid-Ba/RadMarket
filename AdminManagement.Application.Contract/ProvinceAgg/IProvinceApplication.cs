using System.Collections.Generic;
using Framework.Application;
using System.Threading.Tasks;

namespace AdminManagement.Application.Contract.ProvinceAgg
{
    public interface IProvinceApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<ProvinceVM>> GetAll();
        Task<EditProvinceVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditProvinceVM command);
        Task<OperationResult> Create(CreateProvinceVM command);
    }
}
