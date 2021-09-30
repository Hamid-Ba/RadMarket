using AdminManagement.Application.Contract.BannerAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminManagement.Domain.BannerAgg
{
    public interface IBannerRepository : IRepository<Banner>
    {
        Task<EditBannerVM> GetDetailForEditBy(long id);
        Task<IEnumerable<BannerVM>> GetAll();
    }
}
