using Framework.Application;
using StoreManagement.Application.Contract.PackageOrderAgg;
using StoreManagement.Domain.AdtPackageAgg;
using StoreManagement.Domain.PackageAgg;
using StoreManagement.Domain.PackageOrderAgg;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;
using StoreManagement.Domain.StoreAgg;

namespace StoreManagement.Application
{
    public class PackageOrderApplication : IPackageOrderApplication
    {
        private readonly IStoreRepository _storeRepository;
        private readonly IPackageRepository _packageRepository;
        private readonly IAdtPackageRepository _adtPackageRepository;
        private readonly IPackageOrderRepository _packageOrderRepository;

        public PackageOrderApplication(IStoreRepository storeRepository, IPackageRepository packageRepository, IAdtPackageRepository adtPackageRepository, 
            IPackageOrderRepository packageOrderRepository)
        {
            _storeRepository = storeRepository;
            _packageRepository = packageRepository;
            _adtPackageRepository = adtPackageRepository;
            _packageOrderRepository = packageOrderRepository;
        }

        public async Task<IEnumerable<PackageOrderVM>> GetAll() => await _packageOrderRepository.GetAll();

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var order = await _packageOrderRepository.GetEntityByIdAsync(id);
            if (order is null) return result.Failed(ApplicationMessage.NotExist);

            order.Delete();
            await _packageOrderRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<long> PlaceOrder(CreatePackageOrderVM command)
        {
            OperationResult result = new();

            if (string.IsNullOrWhiteSpace(command.MobileNumber)) return 0;
            if (string.IsNullOrWhiteSpace(value: command.PayAmount.ToMoney())) return 0;

            var order = new PackageOrder(command.StoreId,command.PackageId,command.Type,command.TotalPrice,command.DiscountPrice,command.PayAmount,command.MobileNumber);

            await _packageOrderRepository.AddEntityAsync(order);
            await _packageOrderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OperationResult> PaymentSucceeded(long id, long refId)
        {
            OperationResult result = new();

            var order = await _packageOrderRepository.GetEntityByIdAsync(id);

            order.PaymentSucceeded(refId);

            var store = await _storeRepository.GetEntityByIdAsync(order.StoreId);

            switch (order.Type)
            {
                case PackageType.Products:
                    var package = await _packageRepository.GetEntityByIdAsync(order.PackageId);
                    package.AddOrder();
                    store.Charge(30, package.PackagesCount);
                    break;
                case PackageType.Adt:
                    var adtPackage = await _adtPackageRepository.GetEntityByIdAsync(order.PackageId);
                    adtPackage.AddOrder();
                    store.ChargeAdt(14);
                    break;
            }

            await _packageOrderRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<double> GetPriceBy(long id)
        {
            var order = await _packageOrderRepository.GetEntityByIdAsync(id);
            return order.PayAmount;            
        }

        public async Task<IEnumerable<PackageOrderVM>> GetAllBy(long storeId) => await _packageOrderRepository.GetAllBy(storeId);
        
    }
}