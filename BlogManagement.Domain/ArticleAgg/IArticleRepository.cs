using System.Collections.Generic;
using BlogManagement.Application.Contract.ArticleAgg;
using Framework.Domain;
using System.Threading.Tasks;

namespace BlogManagement.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<Article>
    {
        Task<IEnumerable<ArticleVM>> GetAll();
        Task<EditArticleVM> GetDetailForEditBy(long id);
    }
}