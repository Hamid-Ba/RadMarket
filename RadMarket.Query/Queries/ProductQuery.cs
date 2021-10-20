using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiscountManagement.Infrastructure.EfCore;
using Framework.Application;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.CategoryAgg;
using RadMarket.Query.Contracts.ProductAgg;
using StoreManagement.Domain.CategoryAgg;
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

        public async Task<IEnumerable<ProductQueryVM>> GetAll(string filter, string searchTitle = "", int take = 0)
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
                    StoreId = p.StoreId,
                    CategoryId = p.CategoryId,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.MetaDescription.Substring(0, 50) + " ...",
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    OrderCount = p.OrderCount,
                    EachBoxCount = p.EachBoxCount,
                    HasAdtCharge = p.Store.IsAccountStillAdtCharged()
                }).AsNoTracking().ToListAsync();

            products.ForEach(d => d.DiscountRate = discounts.FirstOrDefault(q => q.ProductId == d.Id)?.Rate);

            if (!string.IsNullOrWhiteSpace(searchTitle)) products = products.Where(p => p.Name.Contains(searchTitle)).ToList();
            else
            {
                switch (filter)
                {
                    case "Discount":
                        products = products.Where(p => p.DiscountRate != null || p.DiscountRate > 0).ToList();
                        break;
                    case "BestSells":
                        products = products.OrderByDescending(p => p.OrderCount).ToList();
                        break;
                    case "Adt":
                        products = products.Where(p => p.HasAdtCharge).OrderBy(p => p.OrderCount).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(p => p.Id).ToList();
                        break;
                }
            }

            if (take != 0) products = products.Take(take).ToList();

            return products;
        }

        public async Task<ProductQueryVM> GetBy(long storeId, string slug)
        {
            var product = await _storeContext.Products
                .Include(c => c.Category)
                .Include(s => s.Store)
                .Where(p => p.Store.Status == StoreStatus.Confirmed && p.ProductAcceptanceState == ProductAcceptanceState.Accepted)
                .Select(p => new ProductQueryVM
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    StoreName = p.Store.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Code = p.Code,
                    Name = p.Name,
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Slug = p.Slug,
                    Description = p.Description,
                    EachBoxCount = p.EachBoxCount,
                    MoneyGain = p.MoneyGain,
                    Keywords = p.Keywords,
                    MetaDescription = p.MetaDescription,
                    Category = MapCategory(p.Category)
                }).FirstOrDefaultAsync(p => p.StoreId == storeId && p.Slug == slug);

            if (product != null)
            {
                var discount = await _discountContext.Discounts
                    .Where(d => d.StartDate <= DateTime.Now && d.EndDate >= DateTime.Now)
                    .FirstOrDefaultAsync(d => d.ProductId == product.Id);

                if (discount != null)
                    product.DiscountRate = discount.DiscountRate;
            }

            return product;
        }

        private static CategoryQueryVM MapCategory(Category category) => new CategoryQueryVM
        {
            Id = category.Id,
            Name = category.Name,
            Slug = category.Slug,
        };

        public async Task<IEnumerable<ProductQueryVM>> GetBy(long categoryId, long? productBeRemovedById)
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
                .Where(q => q.CategoryId == categoryId)
                .Where(q => q.Store.Status == StoreStatus.Confirmed && q.ProductAcceptanceState == ProductAcceptanceState.Accepted)
                .Select(p => new ProductQueryVM()
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    CategoryId = p.CategoryId,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.MetaDescription.Substring(0, 50) + " ...",
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    OrderCount = p.OrderCount,
                    EachBoxCount = p.EachBoxCount
                }).AsNoTracking().OrderByDescending(c => c.OrderCount).ToListAsync();

            products.Remove(products.FirstOrDefault(p => p.Id == productBeRemovedById));

            products.ForEach(d => d.DiscountRate = discounts.FirstOrDefault(q => q.ProductId == d.Id)?.Rate);

            return products.Take(3).ToList();
        }

        public async Task<IEnumerable<ProductQueryVM>> GetAllBy(long storeId,long brandId, int take = 0)
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
                .Where(s => s.StoreId == storeId)
                .Where(s => brandId > 0 && s.BrandId == brandId)
                .Where(q => q.Store.Status == StoreStatus.Confirmed && q.ProductAcceptanceState == ProductAcceptanceState.Accepted)
                .Select(p => new ProductQueryVM()
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    BrandId = p.BrandId,
                    CategoryId = p.CategoryId,
                    Picture = p.Picture,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Name = p.Name,
                    Slug = p.Slug,
                    Description = p.MetaDescription.Substring(0, 50) + " ...",
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    OrderCount = p.OrderCount,
                    EachBoxCount = p.EachBoxCount,
                    HasAdtCharge = p.Store.IsAccountStillAdtCharged()
                }).AsNoTracking().OrderByDescending(o => o.Id).ToListAsync();

            products.ForEach(d => d.DiscountRate = discounts.FirstOrDefault(q => q.ProductId == d.Id)?.Rate);

            if (take != 0) products = products.Take(take).ToList();

            return products;
        }
    }
}