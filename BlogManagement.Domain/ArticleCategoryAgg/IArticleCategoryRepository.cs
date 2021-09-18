using Framework.Domain;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<ArticleCategory>
    {
        EditArticleCategoryVM GetDetailForEditBy(long id);
        string GetCategorySlugBy(long id);
    }
}
