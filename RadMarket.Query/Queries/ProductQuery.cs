using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Infrastructure.EfCore;
using Framework.Application;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.ProductAgg;
using StoreManagement.Infrastructure.EfCore;

namespace RadMarket.Query.Queries
{
    public class ProductQuery : IProductQuery
    {
        private readonly StoreContext _storeContext;
        private readonly DiscountContext _discountContext;

        public ProductQuery(StoreContext storeContext, DiscountContext discountContext)
        {
            _storeContext = storeContext;
            _discountContext = discountContext;
        }

        public async Task<IEnumerable<ProductQueryVM>> GetAll(string filter, int take = 0)
        {
            var discounts = await _discountContext.Discounts.Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now).
                Select(d => new
                {
                    Id = d.Id,
                    ProductId = d.ProductId,
                    Rate = d.DiscountRate
                }).ToListAsync();

            var products = await _storeContext.Products
                .Include(s => s.Store)
                .Where(q => q.Store.Status == StoreStatus.Confirmed && q.ProductAcceptanceState == ProductAcceptanceState.Accepted)
                .Select(p => new ProductQueryVM()
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Name = p.Name,
                    Description = p.MetaDescription.Substring(0, 20),
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    OrderCount = p.OrderCount
                }).AsNoTracking().ToListAsync();
            
            products.ForEach(d => d.DiscountRate = discounts.FirstOrDefault(q => q.ProductId == d.Id)?.Rate);

            switch (filter)
            {
                case "Discount":
                    products = products.Where(p => p.DiscountRate != null || p.DiscountRate > 0).ToList();
                    break;
                case "BestSells":
                    products = products.OrderByDescending(p => p.OrderCount).ToList();
                    break;
                default:
                    products = products.OrderByDescending(p => p.Id).ToList();
                    break;
            }

            if (take != 0) products = products.Take(take).ToList();

            return products;
        }
        
    }
}
