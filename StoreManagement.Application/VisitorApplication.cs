using Framework.Application;
using StoreManagement.Application.Contract.VisitorAgg;
using StoreManagement.Domain.VisitorAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class VisitorApplication : IVisitorApplication
    {
        private readonly IVisitorRepository _visitorRepository;

        public VisitorApplication(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

        public async Task<OperationResult> Create(CreateVisitorVM command)
        {
            OperationResult result = new();

            if (_visitorRepository.Exists(v => v.Mobile == command.Mobile)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var visitor = new Visitor(command.FullName, command.Mobile);
            await _visitorRepository.AddEntityAsync(visitor);

            await _visitorRepository.SaveChangesAsync();
            return result.Succeeded();

        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetEntityByIdAsync(id);
            if (visitor is null) return result.Failed(ApplicationMessage.NotExist);

            visitor.Delete();

            await _visitorRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<OperationResult> Edit(EditVisitorVM command)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetEntityByIdAsync(command.Id);

            if (visitor is null) return result.Failed(ApplicationMessage.NotExist);
            if (_visitorRepository.Exists(v => v.Mobile == command.Mobile && v.Id != command.Id))
                return result.Failed(ApplicationMessage.DuplicatedModel);

            visitor.Edit(command.FullName,command.Mobile);

            await _visitorRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<IEnumerable<VisitorVM>> GetAll() => await _visitorRepository.GetAll();

        public async Task<EditVisitorVM> GetDetailForEditBy(long id) => await _visitorRepository.GetDetailForEditBy(id);

        public async Task<OperationResult> UserRegistered(string code)
        {
            OperationResult result = new();

            var visitor = await _visitorRepository.GetBy(code);
            if (visitor is null) return result.Failed(ApplicationMessage.NotExist);

            visitor.UserRegistered();
            await _visitorRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}