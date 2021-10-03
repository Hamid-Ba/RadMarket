using Framework.Domain;
using StoreManagement.Application.Contract.AdtPackageAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.AdtPackageAgg
{
    public interface IAdtPackageRepository : IRepository<AdtPackage>
    {
        Task<AdtPackageVM> GetAdtBy(long id);
        Task<IEnumerable<AdtPackageVM>> GetAll();
        Task<EditAdtPackageVM> GetDetailForEditBy(long id);
    }
}
