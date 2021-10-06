using Framework.Domain;
using StoreManagement.Application.Contract.PackageOrderAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.PackageOrderAgg
{
    public interface IPackageOrderRepository : IRepository<PackageOrder>
    {
        Task<IEnumerable<PackageOrderVM>> GetAll();
        Task<IEnumerable<PackageOrderVM>> GetAllBy(long storeId);
    }
}
