using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminManagement.Application.Contract.BannerAgg
{
    public interface IBannerApplication
    {
        Task<IEnumerable<BannerVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditBannerVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditBannerVM command);
        Task<OperationResult> Create(CreateBannerVM command);
    }
}