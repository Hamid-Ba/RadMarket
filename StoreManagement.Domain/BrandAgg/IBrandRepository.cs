using Framework.Application;
using Framework.Domain;
using StoreManagement.Application.Contract.BrandAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.BrandAgg
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<IEnumerable<BrandVM>> GetAll(long storeId);
        Task<OperationResult> Edit(EditBrandVM command);
    }
}
