using BlogManagement.Application.Contract.ArticleAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<EditArticleVM> GetDetailForEditBy(long id);
    }
}