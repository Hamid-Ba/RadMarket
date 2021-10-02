using Framework.Domain;
using StoreManagement.Application.Contract.PackageAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.PackageAgg
{
    public interface IPackageRepository : IRepository<Package>
    {
        Task<PackageVM> GetPlanBy(long id);
        Task<IEnumerable<PackageVM>> GetAll();
        Task<EditPackageVM> GetDetailForEditBy(long id);
    }
}