using AdminManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.BannerAgg;
using StoreManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class BannerQuery : IBannerQuery
    {
        private readonly StoreContext _storeContext;
        private readonly AdminContext _adminContext;

        public BannerQuery(StoreContext storeContext, AdminContext adminContext)
        {
            _storeContext = storeContext;
            _adminContext = adminContext;
        }

        public async Task<IEnumerable<BannerQueryVM>> GetForAdvertising(int take = 3) => await _adminContext.Banners.Where(b => b.IsActive).Select(b => new BannerQueryVM
        {
            Id = b.Id,
            IsActive = b.IsActive,
            ColSize = b.ColSize,
            Picture = b.Picture,
            PictuerAlt = b.PictuerAlt,
            PictureTitle = b.PictureTitle,
            Position = b.Position,
            StoreId = b.StoreId,
            Url = b.Url,
            CreationDate = b.CreationDate
        }).AsNoTracking().OrderByDescending(b => b.CreationDate).Take(take).ToListAsync();
        
    }
}