using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.PackageAgg
{
    public interface IPackageApplication
    {
        Task<PackageVM> GetPlanBy(long id);
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<PackageVM>> GetAll();
        Task<EditPackageVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditPackageVM command);
        Task<OperationResult> Create(CreatePackageVM command);
    }
}
