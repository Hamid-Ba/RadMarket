using Framework.Application;
using StoreManagement.Application.Contract.PackageAgg;
using StoreManagement.Domain.PackageAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class PackageApplication : IPackageApplication
    {
        private readonly IPackageRepository _packageRepository;

        public PackageApplication(IPackageRepository packageRepository) => _packageRepository = packageRepository;

        public async Task<OperationResult> Create(CreatePackageVM command)
        {
            OperationResult result = new();

            if (_packageRepository.Exists(p => p.Cost == command.Cost))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var imageName = Uploader.ImageUploader(command.ImageFile, "Packages", null!);

            var plan = new Package(command.Title, imageName, command.Cost, command.Description);


            await _packageRepository.AddEntityAsync(plan);
            await _packageRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var package = await _packageRepository.GetEntityByIdAsync(id);
            if (package is null) return result.Failed(ApplicationMessage.NotExist);

            Uploader.ImageRemover(package.ImageName);
            package.Delete();

            await _packageRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditPackageVM command)
        {
            OperationResult result = new();

            var package = await _packageRepository.GetEntityByIdAsync(command.Id);

            if (package is null) return result.Failed(ApplicationMessage.NotExist);
            if (_packageRepository.Exists(p => p.Cost == command.Cost && p.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var imageName = Uploader.ImageUploader(command.ImageFile, "Packages", package.ImageName);
            package.Edit(command.Title, imageName, command.Cost, command.Description);

            await _packageRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<IEnumerable<PackageVM>> GetAll() => await _packageRepository.GetAll();

        public async Task<EditPackageVM> GetDetailForEditBy(long id) => await _packageRepository.GetDetailForEditBy(id);

        public async Task<PackageVM> GetPlanBy(long id) => await _packageRepository.GetPlanBy(id);
        
    }
}