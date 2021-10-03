using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.AdtPackageAgg
{
    public interface IAdtPackageApplication
    {
        Task<AdtPackageVM> GetAdtBy(long id);
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<AdtPackageVM>> GetAll();
        Task<EditAdtPackageVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditAdtPackageVM command);
        Task<OperationResult> Create(CreateAdtPackageVM command);
    }
}
