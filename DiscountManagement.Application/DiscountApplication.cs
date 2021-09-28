using System.Collections.Generic;
using DiscountManagement.Application.Contract.DiscountAgg;
using Framework.Application;
using System.Threading.Tasks;
using DiscountManagement.Domain.DiscountAgg;

namespace DiscountManagement.Application
{
    public class DiscountApplication : IDiscountApplication
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountApplication(IDiscountRepository discountRepository) => _discountRepository = discountRepository;

        public async Task<IEnumerable<DiscountVM>> GetAllBy(long storeId) => await _discountRepository.GetAllBy(storeId);

        public async Task<OperationResult> Create(CreateDiscountVM command)
        {
            OperationResult result = new();

            if (_discountRepository.Exists(c => c.StoreId == command.StoreId && c.ProductId == command.ProductId))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var discount = new Discount(command.StoreId, command.ProductId, command.DiscountRate,
                command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);

            await _discountRepository.AddEntityAsync(discount);
            await _discountRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var discount = await _discountRepository.GetEntityByIdAsync(id);
            if (discount is null) return result.Failed(ApplicationMessage.NotExist);

            discount.Delete();
            await _discountRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditDiscountVM command)
        {
            OperationResult result = new();

            var discount = await _discountRepository.GetEntityByIdAsync(command.Id);

            if (discount is null) return result.Failed(ApplicationMessage.NotExist);
            if (_discountRepository.Exists(c => c.StoreId == command.StoreId &&
                c.ProductId == command.ProductId && c.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            discount.Edit(command.ProductId, command.DiscountRate,
                command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Reason);

            await _discountRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditDiscountVM> GetDetailForEditBy(long id, long storeId) => await _discountRepository.GetDetailForEditBy(id, storeId);
    }
}