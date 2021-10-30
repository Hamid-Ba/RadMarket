using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.BrandAgg;
using RadMarket.Query.Contracts.ProductAgg;
using RadMarket.Query.Contracts.StoreAgg;
using StoreManagement.Domain.BrandAgg;
using StoreManagement.Domain.ProductAgg;
using StoreManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class StoreQuery : IStoreQuery
    {
        private readonly StoreContext _storeContext;

        public StoreQuery(StoreContext storeContext) => _storeContext = storeContext;

        public async Task<IEnumerable<StoreQueryVM>> GetAll() => await _storeContext.Stores.Where(s => s.ChargeExpiredDate >= DateTime.Now)
               .Select(s => new StoreQueryVM
               {
                   Id = s.Id,
                   Name = s.Name,
               }).AsNoTracking().ToListAsync();

        public async Task<StoreQueryVM> GetBy(long id) => await _storeContext.Stores.Where(s => s.ChargeExpiredDate >= DateTime.Now)
                .Include(p => p.Products)
                .Include(b => b.Brands)
                .Select(s => new StoreQueryVM
                {
                    Id = s.Id,
                    Name = s.Name,
                    Products = MapProducts(s.Products),
                    Brands = MapBrands(s.Brands)
                }).FirstOrDefaultAsync(s => s.Id == id);

        private static List<BrandQueryVM> MapBrands(List<Brand> brands) => brands.Select(b => new BrandQueryVM
        {
            Id = b.Id,
            StoreId = b.StoreId,
            Name = b.Name
        }).ToList();

        private static List<ProductQueryVM> MapProducts(List<Product> products) => products.Where(p => p.ProductAcceptanceState == Framework.Application.ProductAcceptanceState.Accepted)
            .Select(p => new ProductQueryVM
            {
                Id = p.Id,
                StoreId = p.StoreId,
                Picture = p.Picture,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Name = p.Name,
                Slug = p.Slug,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice
            }).OrderByDescending(p => p.Id).Take(3).ToList();

    }
}