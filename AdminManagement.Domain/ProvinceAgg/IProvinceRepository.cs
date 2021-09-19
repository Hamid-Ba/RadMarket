using AdminManagement.Application.Contract.ProvinceAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace AdminManagement.Domain.ProvinceAgg
{
    public interface IProvinceRepository : IRepository<Province>
    {
        Task<EditProvinceVM> GetDetialForEditBy(long id);
    }
}
