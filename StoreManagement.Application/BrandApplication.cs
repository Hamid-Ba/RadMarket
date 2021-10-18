using Framework.Application;
using StoreManagement.Application.Contract.BrandAgg;
using StoreManagement.Domain.BrandAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class BrandApplication : IBrandApplication
    {
        private readonly IBrandRepository _brandRepository;

        public BrandApplication(IBrandRepository brandRepository) => _brandRepository = brandRepository;

        public async Task<OperationResult> Create(CreateBrandVM command)
        {
            OperationResult result = new();

            if (_brandRepository.Exists(b => b.Name == command.Name && b.StoreId == command.StoreId))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var brand = new Brand(command.StoreId, command.Name);

            await _brandRepository.AddEntityAsync(brand);
            await _brandRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id, long storeId)
        {
            OperationResult result = new();

            var brand = await _brandRepository.GetEntityByIdAsync(id);
            
            if (brand is null) return result.Failed(ApplicationMessage.NotExist);
            if (brand.StoreId != storeId) return result.Failed(ApplicationMessage.NotExist);

            brand.Delete();

            await _brandRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditBrandVM command)
        {
            OperationResult result = new();

            var brand = await _brandRepository.GetEntityByIdAsync(command.Id);

            if (brand is null) return result.Failed(ApplicationMessage.NotExist);
            if (_brandRepository.Exists(b => b.Name == command.Name && b.StoreId == command.StoreId && b.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            brand.Edit(command.Name);
            await _brandRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<BrandVM>> GetAll(long storeId) => await _brandRepository.GetAll(storeId);

        public async Task<EditBrandVM> GetDetailForEditBy(long id, long storeId) => await _brandRepository.GetDetailForEditBy(id, storeId);
    }
}