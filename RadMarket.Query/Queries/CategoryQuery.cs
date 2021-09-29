using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.CategoryAgg;
using StoreManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class CategoryQuery : ICategoryQuery
    {
        private StoreContext _storeContext;

        public CategoryQuery(StoreContext storeContext) => _storeContext = storeContext;

        public async Task<IEnumerable<CategoryQueryVM>> GetAll() => await _storeContext.Categories.Select(c => new CategoryQueryVM
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug
        }).AsNoTracking().ToListAsync();
        
    }
}
