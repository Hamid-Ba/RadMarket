using AccountManagement.Infrastructure.EfCore;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.VisitorAgg;
using StoreManagement.Domain.VisitorAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class VisitorRepository : Repository<Visitor> , IVisitorRepository
    {
        private readonly StoreContext _context;
        private readonly AccountContext _accountContext;

        public VisitorRepository(StoreContext context, AccountContext accountContext) : base(context)
        {
            _context = context;
            _accountContext = accountContext;
        }

        public async Task<IEnumerable<VisitorVM>> GetAll()
        {
            var result = await _context.Visitors.Select(v => new VisitorVM
            {
                Id = v.Id,
                UniqueCode = v.UniqueCode,
                FullName = v.FullName,
                Mobile = v.Mobile,
                UserCount = v.UserCount
            }).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<Visitor> GetBy(string code) => await _context.Visitors.FirstOrDefaultAsync(v => v.UniqueCode == code);

        public async Task<EditVisitorVM> GetDetailForEditBy(long id) => await _context.Visitors.Select(v => new EditVisitorVM
        {
            Id = v.Id,
            FullName = v.FullName,
            Mobile = v.Mobile
        }).FirstOrDefaultAsync(v => v.Id == id);
        
    }
}
