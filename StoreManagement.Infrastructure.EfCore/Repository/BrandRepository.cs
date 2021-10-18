using Framework.Infrastructure;
using StoreManagement.Domain.BrandAgg;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class BrandRepository : Repository<Brand> , IBrandRepository
    {
        private readonly StoreContext _context;

        public BrandRepository(StoreContext context) : base(context) => _context = context;
    }
}
