using AdminManagement.Application.Contract.ProvinceAgg;
using AdminManagement.Domain.ProvinceAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminManagement.Infrastructure.EfCore.Repository
{
    public class ProvinceRepository : Repository<Province>, IProvinceRepository
    {
        private readonly AdminContext _context;

        public ProvinceRepository(AdminContext context) : base(context) => _context = context;


        public async Task<EditProvinceVM> GetDetialForEditBy(long id) => await _context.Provinces.Select(p => new EditProvinceVM
        {
            Id = p.Id,
            Name = p.Name
        }).FirstOrDefaultAsync(p => p.Id == id);

    }
}
