using DiscountManagement.Application.Contract.DiscountCodeAgg;
using DiscountManagement.Domain.DiscountCodeAgg;
using Framework.Application;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DiscountManagement.Application
{
    public class DiscountCodeApplication : IDiscountCodeApplication
    {
        private readonly IDiscountCodeRepository _discountCodeRepository;

        public DiscountCodeApplication(IDiscountCodeRepository discountCodeRepository) => _discountCodeRepository = discountCodeRepository;

        public async Task<OperationResult> Create(CreateDiscountCodeVM command)
        {
            OperationResult result = new();

            if (_discountCodeRepository.Exists(e => e.Code == command.Code))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            var code = new DiscountCode(command.Code,command.Rate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Count, command.Reason);

            await _discountCodeRepository.AddEntityAsync(code);
            await _discountCodeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var code = await _discountCodeRepository.GetEntityByIdAsync(id);
            if (code is null) return result.Failed(ApplicationMessage.NotExist);

            code.Delete();
            await _discountCodeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditDiscountCodeVM command)
        {
            OperationResult result = new();

            var code = await _discountCodeRepository.GetEntityByIdAsync(command.Id);

            if (code is null) return result.Failed(ApplicationMessage.NotExist);
            if (_discountCodeRepository.Exists(e => e.Code == command.Code && e.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            code.Edit(command.Code,command.Rate, command.StartDate.ToGeorgianDateTime(), command.EndDate.ToGeorgianDateTime(), command.Count, command.Reason);
            await _discountCodeRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<IEnumerable<DiscountCodeVM>> GetAll() => await _discountCodeRepository.GetAll();

        public async Task<EditDiscountCodeVM> GetDetailForEditBy(long id) => await _discountCodeRepository.GetDetailForEditBy(id);
        
    }
}