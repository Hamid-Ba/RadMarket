using System.Collections.Generic;
using BlogManagement.Application.Contract.ArticleCategoryAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {
        string GetCategorySlugBy(long id);
        Task<IEnumerable<ArticleCategoryVM>> GetAll();
        Task<EditArticleCategoryVM> GetDetailForEditBy(long id);
    }
}