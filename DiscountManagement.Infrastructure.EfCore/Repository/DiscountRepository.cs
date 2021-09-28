using System.Collections.Generic;
using System.Linq;
using DiscountManagement.Application.Contract.DiscountAgg;
using DiscountManagement.Domain.DiscountAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using StoreManagement.Infrastructure.EfCore;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly DiscountContext _context;
        private readonly StoreContext _storeContext;

        public DiscountRepository(DiscountContext context, StoreContext storeContext) : base(context)
        {
            _context = context;
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<DiscountVM>> GetAllBy(long storeId)
        {
            var products = await _storeContext.Products.Where(p => p.StoreId == storeId)
                .Select(p => new { Id = p.Id, Name = p.Name }).ToListAsync();

            var result = await _context.Discounts.Where(s => s.StoreId == storeId).Select(c =>
               new DiscountVM()
               {
                   Id = c.Id,
                   StoreId = c.StoreId,
                   ProductId = c.ProductId,
                   DiscountRate = c.DiscountRate,
                   StartDate = c.StartDate.ToFarsi(),
                   EndDate = c.EndDate.ToFarsi(),
                   Reason = c.Reason
               }).AsNoTracking().ToListAsync();


            foreach (var discount in result)
                discount.ProductName = products.Find(p => p.Id == discount.ProductId)?.Name;

            return result;
        }


        public async Task<EditDiscountVM> GetDetailForEditBy(long id, long storeId)
        {
            var discount = await _context.Discounts.FirstOrDefaultAsync(d => d.Id == id);

            if (discount.StoreId != storeId) return null;

            var result = new EditDiscountVM
            {
                Id = discount.Id,
                StoreId = discount.StoreId,
                ProductId = discount.ProductId,
                DiscountRate = discount.DiscountRate,
                StartDate = discount.StartDate.ToFarsi(),
                EndDate = discount.EndDate.ToFarsi(),
                Reason = discount.Reason
            };

            return result;
        }
    }
}
