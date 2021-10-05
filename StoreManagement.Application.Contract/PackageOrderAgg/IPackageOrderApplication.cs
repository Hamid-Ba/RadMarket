using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.PackageOrderAgg
{
    public interface IPackageOrderApplication
    {
        Task<OperationResult> Delete(long id);
        Task<IEnumerable<PackageOrderVM>> GetAll();
        Task<long> PlaceOrder(CreatePackageOrderVM command);
        Task<OperationResult> PaymentSucceeded(long id, long refId);
    }
}