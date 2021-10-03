using Framework.Application;
using StoreManagement.Application.Contract.AdtPackageAgg;
using StoreManagement.Domain.AdtPackageAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class AdtPackageApplication : IAdtPackageApplication
    {
        private readonly IAdtPackageRepository _adtPackageRepository;

        public AdtPackageApplication(IAdtPackageRepository adtPackageRepository) => _adtPackageRepository = adtPackageRepository;

        public async Task<OperationResult> Create(CreateAdtPackageVM command)
        {
            OperationResult result = new();

            if (_adtPackageRepository.Exists(p => p.Cost == command.Cost && p.Type == command.Type))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var imageName = Uploader.ImageUploader(command.ImageFile, "AdtPackages", null!);
            var plan = new AdtPackage(command.Title, imageName, command.Type, command.Cost, command.Description);

            await _adtPackageRepository.AddEntityAsync(plan);
            await _adtPackageRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var package = await _adtPackageRepository.GetEntityByIdAsync(id);
            if (package is null) return result.Failed(ApplicationMessage.NotExist);

            Uploader.ImageRemover(package.ImageName);
            package.Delete();

            await _adtPackageRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditAdtPackageVM command)
        {
            OperationResult result = new();

            var adt = await _adtPackageRepository.GetEntityByIdAsync(command.Id);

            if (adt is null) return result.Failed(ApplicationMessage.NotExist);
            if (_adtPackageRepository.Exists(p => p.Cost == command.Cost && p.Type == command.Type && p.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var imageName = Uploader.ImageUploader(command.ImageFile, "AdtPackages", adt.ImageName);
            adt.Edit(command.Title, imageName, command.Type, command.Cost, command.Description);

            await _adtPackageRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<AdtPackageVM> GetAdtBy(long id) => await _adtPackageRepository.GetAdtBy(id);

        public async Task<IEnumerable<AdtPackageVM>> GetAll() => await _adtPackageRepository.GetAll();

        public async Task<EditAdtPackageVM> GetDetailForEditBy(long id) => await _adtPackageRepository.GetDetailForEditBy(id);
        
    }
}