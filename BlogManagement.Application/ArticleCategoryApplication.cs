using System.Collections.Generic;
using BlogManagement.Application.Contract.ArticleCategoryAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Application;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleCategoryApplication : IArticleCategoryApplication
    {
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleCategoryApplication(IArticleCategoryRepository articleCategoryRepository) => _articleCategoryRepository = articleCategoryRepository;

        public async Task<OperationResult> Create(CreateArticleCategoryVM command)
        {
            OperationResult result = new();

            if (_articleCategoryRepository.Exists(c => c.Name == command.Name))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var category = new ArticleCategory(command.Name, command.Description, command.ShowOrder, command.Slug.Slugify(), command.Keywords, command.MetaDescription);

            await _articleCategoryRepository.AddEntityAsync(category);
            await _articleCategoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var category = await _articleCategoryRepository.GetEntityByIdAsync(id);
            if (category is null) return result.Failed(ApplicationMessage.NotExist);

            category.Delete();
            await _articleCategoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<ArticleCategoryVM>> GetAll() => await _articleCategoryRepository.GetAll();

        public async Task<OperationResult> Edit(EditArticleCategoryVM command)
        {
            OperationResult result = new();

            var category = await _articleCategoryRepository.GetEntityByIdAsync(command.Id);
            if (category is null) return result.Failed(ApplicationMessage.NotExist);
            if (_articleCategoryRepository.Exists(c => c.Name == command.Name && c.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            category.Edit(command.Name, command.Description, command.ShowOrder, command.Slug.Slugify(), command.Keywords, command.MetaDescription);
            await _articleCategoryRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditArticleCategoryVM> GetDetailForEditBy(long id) => await _articleCategoryRepository.GetDetailForEditBy(id);
        
    }
}