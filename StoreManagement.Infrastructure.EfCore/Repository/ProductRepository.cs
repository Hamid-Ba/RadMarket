using System.Collections.Generic;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Domain.ProductAgg;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context) : base(context) => _context = context;

        public async Task<IEnumerable<ProductVM>> GetAll() => await _context.Products.Include(c => c.Category).Select(
            p => new ProductVM()
            {
                Id = p.Id,
                StoreId = p.StoreId,
                Name = p.Name,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                Code = p.Code,
                Stock = p.Stock,
                ConsumerPrice = p.ConsumerPrice,
                PurchasePrice = p.PurchacePrice,
                MoneyGain = p.MoneyGain,
                ProductAcceptanceState = p.ProductAcceptanceState,
                ProductAcceptOrRejectDescription = p.ProductAcceptOrRejectDescription
            }).AsNoTracking().ToListAsync();
        

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
        
    }
}
