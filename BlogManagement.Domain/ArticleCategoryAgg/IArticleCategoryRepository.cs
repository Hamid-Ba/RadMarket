using BlogManagement.Application.Contract.ArticleCategoryAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {
        Task<EditArticleCategoryVM> GetDetailForEditBy(long id);
        string GetCategorySlugBy(long id);
    }
}