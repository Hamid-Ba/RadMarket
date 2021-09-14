using DiscountManagement.Application.Contract.DiscountAgg;
using DiscountManagement.Domain.DiscountAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class DiscountRepository : Repository<Discount>, IDiscountRepository
    {
        private readonly DiscountContext _context;

        public DiscountRepository(DiscountContext context) : base(context) => _context = context;

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
