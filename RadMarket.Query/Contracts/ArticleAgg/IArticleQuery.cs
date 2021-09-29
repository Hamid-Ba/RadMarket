using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.ArticleAgg
{
    public interface IArticleQuery
    {
        Task<ArticleQueryVM> GetBy(string slug);
        Task<IEnumerable<ArticleQueryVM>> GetAll();
        Task<IEnumerable<ArticleQueryVM>> GetForSideBar(int take = 5);
    }
}
