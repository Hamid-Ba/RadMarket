using AdminManagement.Application.Contract.BannerAgg;
using AdminManagement.Domain.BannerAgg;
using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdminManagement.Application
{
    public class BannerApplication : IBannerApplication
    {
        private readonly IBannerRepository _bannerRepository;

        public BannerApplication(IBannerRepository bannerRepository) => _bannerRepository = bannerRepository;

        public async Task<OperationResult> Create(CreateBannerVM command)
        {
            var result = new OperationResult();

            try
            {
                var pictureName = Uploader.ImageUploader(command.Picture, "Banners", null!);

                var banner = new Banner(command.StoreId,pictureName, command.PictuerAlt, command.PictureTitle, command.ColSize, command.Url, command.Position);

                await _bannerRepository.AddEntityAsync(banner);
                await _bannerRepository.SaveChangesAsync();

                return result.Succeeded();
            }
            catch { return result.Failed(ApplicationMessage.GoesWrong); }
        }

        public async Task<OperationResult> Delete(long id)
        {
            var result = new OperationResult();

            try
            {
                var banner = await _bannerRepository.GetEntityByIdAsync(id);

                if (banner is null) return result.Failed(ApplicationMessage.NotExist);

                Uploader.ImageRemover(banner.Picture);
                banner.Delete();

                await _bannerRepository.SaveChangesAsync();

                return result.Succeeded();
            }
            catch { return result.Failed(ApplicationMessage.GoesWrong); }
        }

        public async Task<OperationResult> Edit(EditBannerVM command)
        {
            var result = new OperationResult();

            try
            {
                var banner = await _bannerRepository.GetEntityByIdAsync(command.Id);

                if (banner is null) return result.Failed(ApplicationMessage.NotExist);

                var pictureName = Uploader.ImageUploader(command.Picture, "Banners", banner.Picture);
                banner.Edit(command.StoreId,pictureName, command.PictuerAlt, command.PictureTitle, command.ColSize, command.Url, command.Position);

                await _bannerRepository.SaveChangesAsync();

                return result.Succeeded();
            }
            catch { return result.Failed(ApplicationMessage.GoesWrong); }
        }

        public async Task<IEnumerable<BannerVM>> GetAll() => await _bannerRepository.GetAll();

        public async Task<EditBannerVM> GetDetailForEditBy(long id) => await _bannerRepository.GetDetailForEditBy(id);
    }
}