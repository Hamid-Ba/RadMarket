using Framework.Domain;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<Article>
    {
        EditArticleVM GetDetailForEditBy(long id);
    }
}
