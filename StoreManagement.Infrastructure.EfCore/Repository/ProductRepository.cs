using System.Collections.Generic;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Domain.ProductAgg;
using System.Linq;
using System.Threading.Tasks;
using Framework.Application;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context) : base(context) => _context = context;

        public async Task<bool> IsProductBelongToStore(long id, long storeId) => await _context.Products.AnyAsync(p => p.Id == id && p.StoreId == storeId);

        public async Task<IEnumerable<ProductVM>> GetAll(SearchStoreVM search)
        {
            var result = _context.Products.Include(s => s.Store).Include(c => c.Category).Select(
                p => new ProductVM()
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    StoreName = p.Store.Name,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Code = p.Code,
                    Stock = p.Stock,
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    MoneyGain = p.MoneyGain,
                    ProductAcceptanceState = p.ProductAcceptanceState,
                    ProductAcceptOrRejectDescription = p.ProductAcceptOrRejectDescription,
                    CreationDate = p.CreationDate.ToFarsi(),
                    Description = p.Description,
                    EachBoxCount = p.EachBoxCount,
                    Keywords = p.Keywords,
                    MetaDescription = p.MetaDescription,
                    Picture = p.Picture,
                    Slug = p.Slug,
                    PictureAlt = p.PictureAlt,
                    PictureTitle = p.PictureTitle,
                    Prize = p.Prize
                }).AsNoTracking().AsQueryable();

            if (search != null && !string.IsNullOrWhiteSpace(search.Code)) result = result.Where(p => p.Code.Contains(search.Code));

            return await result.ToListAsync();
        }


        public async Task<ProductVM> GetDetailBy(long id) => await _context.Products
            .Include(s => s.Store)
            .Include(c => c.Category)
            .Select(p => new ProductVM()
            {
                Id = p.Id,
                StoreId = p.StoreId,
                StoreName = p.Store.Name,
                Name = p.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Code = p.Code,
                Stock = p.Stock,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice,
                MoneyGain = p.MoneyGain,
                ProductAcceptanceState = p.ProductAcceptanceState,
                ProductAcceptOrRejectDescription = p.ProductAcceptOrRejectDescription,
                CreationDate = p.CreationDate.ToFarsi(),
                Description = p.Description,
                EachBoxCount = p.EachBoxCount,
                Keywords = p.Keywords,
                MetaDescription = p.MetaDescription,
                Picture = p.Picture,
                Slug = p.Slug,
                PictureAlt = p.PictureAlt,
                PictureTitle = p.PictureTitle,
                Prize = p.Prize
            }).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<EditProductVM> GetDetailForEditBy(long id) => await _context.Products.Select(p => new EditProductVM
        {
            Id = p.Id,
            StoreId = p.StoreId,
            CategoryId = p.CategoryId,
            Code = p.Code,
            Name = p.Name,
            ConsumerPrice = p.ConsumerPrice,
            Description = p.Description,
            EachBoxCount = p.EachBoxCount,
            Keywords = p.Keywords,
            MetaDescription = p.MetaDescription,
            PictureName = p.Picture,
            PictureAlt = p.PictureAlt,
            PictureTitle = p.PictureTitle,
            Prize = p.Prize,
            PurchacePrice = p.PurchacePrice,
            Slug = p.Slug,
            Stock = p.Stock
        }).FirstOrDefaultAsync(p => p.Id == id);

        public async Task<IEnumerable<ProductVM>> GetAll(long storeId, SearchStoreVM search)
        {
            var result = _context.Products.Where(s => s.StoreId == storeId).Include(s => s.Store)
                .Include(c => c.Category).Select(
                p => new ProductVM()
                {
                    Id = p.Id,
                    StoreId = p.StoreId,
                    StoreName = p.Store.Name,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = p.Category.Name,
                    Code = p.Code,
                    Stock = p.Stock,
                    ConsumerPrice = p.ConsumerPrice,
                    PurchasePrice = p.PurchacePrice,
                    MoneyGain = p.MoneyGain,
                    ProductAcceptanceState = p.ProductAcceptanceState,
                    ProductAcceptOrRejectDescription = p.ProductAcceptOrRejectDescription,
                    CreationDate = p.CreationDate.ToFarsi()
                }).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(search.Code)) result = result.Where(p => p.Code.Contains(search.Code));

            return await result.ToListAsync();
        }

    }
}