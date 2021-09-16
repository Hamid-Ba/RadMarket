using Framework.Application;
using System.Threading.Tasks;

namespace AdminManagement.Application.Contract.BannerAgg
{
    public interface IBannerApplication
    {
        Task<OperationResult> Delete(long id);
        Task<EditBannerVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditBannerVM command);
        Task<OperationResult> Create(CreateBannerVM command);
    }
}