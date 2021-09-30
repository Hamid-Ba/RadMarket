using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.CategoryAgg;
using RadMarket.Query.Contracts.ProductAgg;
using StoreManagement.Domain.ProductAgg;
using StoreManagement.Infrastructure.EfCore;
using Framework.Application;
using Framework.Domain;
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

        public async Task<CategoryQueryVM> GetAllProducts(string slug) => await _storeContext.Categories.Include(p => p.Products).ThenInclude(s => s.Store).Select(c => new CategoryQueryVM
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            KeyWords = c.KeyWords,
            MetaDescription = c.MetaDescription,
            Description = c.Description,
            Products = MapProducts(c.Products)
        }).FirstOrDefaultAsync(c => c.Slug == slug);

        private static List<ProductQueryVM> MapProducts(List<Product> products) => products
            .Where(p => p.Store.Status == StoreStatus.Confirmed && p.ProductAcceptanceState == ProductAcceptanceState.Accepted)
            .Select(p => new ProductQueryVM
            {
                Id = p.Id,
                StoreId = p.StoreId,
                CategoryId = p.CategoryId,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Name = p.Name,
                Slug = p.Slug,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice,
                OrderCount = p.OrderCount
            }).ToList();
    }
}