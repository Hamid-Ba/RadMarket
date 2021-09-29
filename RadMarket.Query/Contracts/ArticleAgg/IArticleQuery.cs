using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.ArticleAgg
{
    public interface IArticleQuery
    {
        Task<IEnumerable<ArticleQueryVM>> GetAll();
    }
}
