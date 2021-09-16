using AdminManagement.Application.Contract.BannerAgg;
using AdminManagement.Domain.BannerAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminManagement.Infrastructure.EfCore.Repository
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        private readonly AdminContext _context;

        public BannerRepository(AdminContext context) : base(context) => _context = context;

        public async Task<EditBannerVM> GetDetailForEditBy(long id) => await _context.Banners.Select(b => new EditBannerVM
        {
            Id = b.Id,
            PictureName = b.Picture,
            PictuerAlt = b.PictuerAlt,
            PictureTitle = b.PictureTitle,
            ColSize = b.ColSize,
            Position = b.Position,
            Url = b.Url
        }).FirstOrDefaultAsync(b => b.Id == id);

    }
}