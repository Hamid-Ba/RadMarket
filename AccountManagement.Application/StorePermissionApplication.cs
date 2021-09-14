using AccountManagement.Application.Contract.StorePermissionAgg;
using AccountManagement.Domain.StorePermissionAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class StorePermissionApplication : IStorePermissionApplication
    {
        private readonly IStorePermissionRepository _storePermissionRepository;

        public StorePermissionApplication(IStorePermissionRepository storePermissionRepository) => _storePermissionRepository = storePermissionRepository;

        public async Task<IEnumerable<StorePermissionVM>> GetAll() => await _storePermissionRepository.GetAll();

    }
}
