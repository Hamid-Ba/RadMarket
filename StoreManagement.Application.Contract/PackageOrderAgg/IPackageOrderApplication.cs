using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.PackageOrderAgg
{
    public interface IPackageOrderApplication
    {
        Task<double> GetPriceBy(long id);
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<PackageOrderVM>> GetAll();
        Task<long> PlaceOrder(CreatePackageOrderVM command);
        Task<IEnumerable<PackageOrderVM>> GetAllBy(long storeId);
        Task<OperationResult> PaymentSucceeded(long id, long refId);
    }
}