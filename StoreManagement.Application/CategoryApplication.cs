using System.Collections.Generic;
using Framework.Application;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Domain.CategoryAgg;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class CategoryApplication : ICategoryApplication
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryApplication(ICategoryRepository categoryRepository) => _categoryRepository = categoryRepository;

        public async Task<OperationResult> Create(CreateCategoryVM command)
        {
            OperationResult result = new();

            if (_categoryRepository.Exists(c => c.Name == command.Name || c.Slug == command.Slug))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var category = new Category(command.Name, command.Description, command.KeyWords, command.MetaDescription, command.Slug.Slugify());

            await _categoryRepository.AddEntityAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var category = await _categoryRepository.GetEntityByIdAsync(id);
            if (category is null) return result.Failed(ApplicationMessage.NotExist);

            category.Delete();
            await _categoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<CategoryVM>> GetAll() => await _categoryRepository.GetAll();

        public async Task<OperationResult> Edit(EditCategoryVM command)
        {
            OperationResult result = new();

            var category = await _categoryRepository.GetEntityByIdAsync(command.Id);

            if (category is null) return result.Failed(ApplicationMessage.NotExist);
            if (_categoryRepository.Exists(c => (c.Name == command.Name || c.Slug == command.Slug) && c.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            category.Edit(command.Name, command.Description, command.KeyWords, command.MetaDescription, command.Slug.Slugify());
            await _categoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditCategoryVM> GetDetailForEditBy(long id) => await _categoryRepository.GetDetailForEditBy(id);
        
    }
}
