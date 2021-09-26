using System.Collections.Generic;
using BlogManagement.Application.Contract.ArticleAgg;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Application;
using System.Threading.Tasks;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public async Task<OperationResult> Create(CreateArticleVM command)
        {
            OperationResult result = new OperationResult();

            if (_articleRepository.Exists(a => a.Title == command.Title))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetCategorySlugBy(command.ArticleCategoryId);
            var path = $"{categorySlug}/{slug}";
            var pictureName = Uploader.ImageUploader(command.Picture, path, null!);

            var article = new Article(command.Title, command.ArticleCategoryId, pictureName, command.PictureAlt,
                command.PictureTitle, command.PublishDate.ToGeorgianDateTime(),command.Author, command.ShortDescription,
                command.Description, slug, command.Keywords, command.MetaDescription);

            await _articleRepository.AddEntityAsync(article);
            await _articleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new OperationResult();

            var article = await _articleRepository.GetEntityByIdAsync(id);
            if (article is null) return result.Failed(ApplicationMessage.NotExist);

            var slug = article.Slug;
            var categorySlug = _articleCategoryRepository.GetCategorySlugBy(article.ArticleCategoryId);
            var folderName = $"{categorySlug}\\{slug}";

            article.Delete();
            Uploader.DirectoryRemover(folderName);

            await _articleRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<IEnumerable<ArticleVM>> GetAll() => await _articleRepository.GetAll();

        public async Task<OperationResult> Edit(EditArticleVM command)
        {
            OperationResult result = new OperationResult();

            var article = await _articleRepository.GetEntityByIdAsync(command.Id);
            
            if (article == null) return result.Failed(ApplicationMessage.NotExist);
            if (_articleRepository.Exists(a => a.Title == command.Title && a.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var slug = command.Slug.Slugify();
            var categorySlug = _articleCategoryRepository.GetCategorySlugBy(command.ArticleCategoryId);
            var path = $"{categorySlug}/{slug}";
            var pictureName = Uploader.ImageUploader(command.Picture, path, article.PictureName);

            article.Edit(command.Title, command.ArticleCategoryId, pictureName, command.PictureAlt,
                command.PictureTitle, command.PublishDate.ToGeorgianDateTime(),command.Author, command.ShortDescription,
                command.Description, slug, command.Keywords, command.MetaDescription);
            await _articleRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditArticleVM> GetDetailForEditBy(long id) => await _articleRepository.GetDetailForEditBy(id);
        
    }
}