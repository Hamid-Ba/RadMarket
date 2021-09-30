using AdminManagement.Application.Contract.BannerAgg;
using AdminManagement.Domain.BannerAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminManagement.Infrastructure.EfCore.Repository
{
    public class BannerRepository : Repository<Banner>, IBannerRepository
    {
        private readonly AdminContext _context;
        private readonly StoreContext _storeContext;

        public BannerRepository(AdminContext context, StoreContext storeContext) : base(context)
        {
            _context = context;
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<BannerVM>> GetAll()
        {
            var stores = await _storeContext.Stores.Select(s => new { Id = s.Id, Name = s.Name }).ToListAsync();

            var banners = await _context.Banners.Select(b => new BannerVM
            {
                Id = b.Id,
                StoreId = b.StoreId,
                ColSize = b.ColSize,
                Picture = b.Picture,
                PictuerAlt = b.PictuerAlt,
                PictureTitle = b.PictureTitle,
                Position = b.Position,
                Url = b.Url,
                CreatedDate = b.CreationDate.ToFarsi(),
                LastUpdatedDate = b.LastUpdateDate.ToFarsi(),
                IsActive = b.IsActive
            }).AsNoTracking().ToListAsync();

            banners.ForEach(b => b.StoreName = stores.FirstOrDefault(s => s.Id == b.StoreId)?.Name);

            return banners;
        }

        public async Task<EditBannerVM> GetDetailForEditBy(long id) => await _context.Banners.Select(b => new EditBannerVM
        {
            Id = b.Id,
            StoreId = b.StoreId,
            PictureName = b.Picture,
            PictuerAlt = b.PictuerAlt,
            PictureTitle = b.PictureTitle,
            ColSize = b.ColSize,
            Position = b.Position,
            Url = b.Url
        }).FirstOrDefaultAsync(b => b.Id == id);

    }
}