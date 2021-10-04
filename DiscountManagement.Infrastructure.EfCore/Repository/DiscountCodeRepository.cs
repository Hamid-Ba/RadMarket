using DiscountManagement.Application.Contract.DiscountCodeAgg;
using DiscountManagement.Domain.DiscountCodeAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscountManagement.Infrastructure.EfCore.Repository
{
    public class DiscountCodeRepository : Repository<DiscountCode>, IDiscountCodeRepository
    {
        private readonly DiscountContext _context;

        public DiscountCodeRepository(DiscountContext context) : base(context) => _context = context;

        public async Task<IEnumerable<DiscountCodeVM>> GetAll() => await _context.DiscountCodes.Select(c => new DiscountCodeVM
        {
            Id = c.Id,
            Code = c.Code,
            Count = c.Count,
            Rate = c.Rate,
            StartDate = c.StartDate.ToFarsi(),
            EndDate = c.EndDate.ToFarsi(),
            Reason = c.Reason
        }).AsNoTracking().ToListAsync();

        public async Task<EditDiscountCodeVM> GetDetailForEditBy(long id) => await _context.DiscountCodes.Select(c => new EditDiscountCodeVM
        {
            Id = c.Id,
            Code = c.Code,
            Count = c.Count,
            Rate = c.Rate,
            StartDate = c.StartDate.ToFarsi(),
            EndDate = c.EndDate.ToFarsi(),
            Reason = c.Reason
        }).FirstOrDefaultAsync(c => c.Id == id);
    }
}