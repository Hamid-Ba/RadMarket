using System.Collections.Generic;
using AdminManagement.Application.Contract.ProvinceAgg;
using AdminManagement.Domain.ProvinceAgg;
using Framework.Application;
using System.Threading.Tasks;

namespace AdminManagement.Application
{
    public class ProvinceApplication : IProvinceApplication
    {
        private readonly IProvinceRepository _provinceRepository;

        public ProvinceApplication(IProvinceRepository provinceRepository) => _provinceRepository = provinceRepository;

        public async Task<OperationResult> Create(CreateProvinceVM command)
        {
            OperationResult result = new();

            if (_provinceRepository.Exists(p => p.Name == command.Name)) return result.Failed(ApplicationMessage.DuplicatedModel);

            var province = new Province(command.Name);

            await _provinceRepository.AddEntityAsync(province);
            await _provinceRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var province = await _provinceRepository.GetEntityByIdAsync(id);
            if (province is null) return result.Failed(ApplicationMessage.NotExist);

            province.Delete();
            await _provinceRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<ProvinceVM>> GetAll() => await _provinceRepository.GetAll();

        public async Task<OperationResult> Edit(EditProvinceVM command)
        {
            OperationResult result = new();

            var province = await _provinceRepository.GetEntityByIdAsync(command.Id);

            if (province is null) return result.Failed(ApplicationMessage.NotExist);
            if (_provinceRepository.Exists(p => p.Name == command.Name && p.Id != command.Id)) return result.Failed(ApplicationMessage.DuplicatedModel);

            province.Edit(command.Name);
            await _provinceRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditProvinceVM> GetDetailForEditBy(long id) => await _provinceRepository.GetDetailForEditBy(id);
    }
}