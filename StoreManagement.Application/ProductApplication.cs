using System.Collections.Generic;
using Framework.Application;
using StoreManagement.Application.Contract.ProductAgg;
using StoreManagement.Domain.ProductAgg;
using System.Threading.Tasks;
using StoreManagement.Domain.StoreAgg;

namespace StoreManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;

        public ProductApplication(IProductRepository productRepository) => _productRepository = productRepository;

        public async Task<OperationResult> Create(CreateProductVM command)
        {
            OperationResult result = new();

            if (string.IsNullOrWhiteSpace(command.Description))
                return result.Failed("درباره محصول نمی تواند خالی باشد");

            if (_productRepository.Exists(p => p.Code == command.Code && p.StoreId == command.StoreId))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            if (_productRepository.Exists(p => p.Slug == command.Slug && p.StoreId == command.StoreId))
                return result.Failed(ApplicationMessage.SlugIsExist);

            if (command.CategoryId <= 0) return result.Failed("لطفا دسته محصول را انتخاب کنید !");
            if (command.BrandId <= 0) return result.Failed("لطفا برند را انتخاب کنید !");

            var picture = Uploader.ImageUploader(command.Picture, $"{command.StoreId}//{command.Name}", null!);

            var product = new Product(command.StoreId,command.BrandId, command.CategoryId, command.Code, command.Name, picture, command.PictureAlt,
                command.PictureTitle, command.EachBoxCount, command.ConsumerPrice, command.PurchacePrice, command.Stock, command.Prize,
                command.Description, command.Slug.Slugify(), command.Keywords, command.MetaDescription,
                "محصول ایجاد شده", ProductAcceptanceState.UnderProgress);

            await _productRepository.AddEntityAsync(product);
            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<bool> IsProductBelongToStore(long id, long storeId) => await _productRepository.IsProductBelongToStore(id, storeId);

        public async Task<IEnumerable<ProductVM>> GetAll(SearchStoreVM search) => await _productRepository.GetAll(search);

        public async Task<OperationResult> ChangeStatus(ChangeStatusProductVM command)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(command.Id);
            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            if (command.State == ProductAcceptanceState.Rejected && string.IsNullOrWhiteSpace(command.Description))
                command.Description = "محصول رد شده";

            product.SetProductState(command.State, command.Description);
            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<ProductVM>> GetAll(long storeId, SearchStoreVM search) => await _productRepository.GetAll(storeId, search);

        public async Task<ProductVM> GetDetailBy(long id) => await _productRepository.GetDetailBy(id);

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(id);
            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            Uploader.ImageRemover(product.Picture);
            product.Delete();

            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditProductVM command)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(command.Id);

            if (product is null) return result.Failed(ApplicationMessage.NotExist);
            if (_productRepository.Exists(p => p.Code == command.Code && p.StoreId == command.StoreId && p.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            if (_productRepository.Exists(p => p.Slug == command.Slug && p.StoreId == command.StoreId && p.Id != command.Id))
                return result.Failed(ApplicationMessage.SlugIsExist);

            if (command.CategoryId <= 0) return result.Failed("لطفا دسته محصول را انتخاب کنید !");
            if (command.BrandId <= 0) return result.Failed("لطفا برند را انتخاب کنید !");

            var picture = Uploader.ImageUploader(command.Picture, $"{command.StoreId}//{command.Name}", product.Picture);

            product.Edit(command.BrandId,command.CategoryId, command.Code, command.Name, picture, command.PictureAlt,
                command.PictureTitle, command.EachBoxCount, command.ConsumerPrice, command.PurchacePrice, command.Stock, command.Prize,
                command.Description, command.Slug.Slugify(), command.Keywords, command.MetaDescription);

            product.SetProductState(ProductAcceptanceState.UnderProgress, "محصول ویرایش شده");

            await _productRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> CheckCountOfProduct(long id, int count)
        {
            OperationResult result = new();

            var product = await _productRepository.GetEntityByIdAsync(id);
            if (product is null) return result.Failed(ApplicationMessage.NotExist);

            if (product.Stock < count) return result.Failed($"تعداد انتخاب شده از محصول {product.Name} بیش از موجودی در انبار هست");

            return result.Succeeded();
        }

        public async Task<EditProductVM> GetDetailForEditBy(long id) => await _productRepository.GetDetailForEditBy(id);

        public async Task<string> GetProductSlugBy(long id) => await _productRepository.GetProductSlugBy(id);

        public async Task<long> GetProductStoreIdBy(long id) => await _productRepository.GetProductStoreIdBy(id);


    }
}
