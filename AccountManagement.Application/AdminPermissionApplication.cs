using AccountManagement.Application.Contract.AdminPermissionAgg;
using AccountManagement.Domain.AdminPermissionAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class AdminPermissionApplication : IAdminPermissionApplication
    {
        private readonly IAdminPermissionRepository _adminPermissionRepository;

        public AdminPermissionApplication(IAdminPermissionRepository adminPermissionRepository) => _adminPermissionRepository = adminPermissionRepository;

        public async Task<IEnumerable<AdminPermissionVM>> GetAll() => await _adminPermissionRepository.GetAll();
        
    }
}