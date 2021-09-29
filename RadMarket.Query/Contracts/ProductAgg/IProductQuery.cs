using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.ProductAgg
{
    public interface IProductQuery
    {
        Task<IEnumerable<ProductQueryVM>> GetAll(string filter,int take = 0);
        //Task<IEnumerable<ProductQueryVM>> GetAllBestSells(int take = 0);
        //Task<IEnumerable<ProductQueryVM>> GetAllWhichHasDiscount(int take = 0);
    }
}
