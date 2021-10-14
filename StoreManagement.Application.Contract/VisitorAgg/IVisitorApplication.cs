using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Application.Contract.VisitorAgg
{
    public interface IVisitorApplication
    {
        Task<IEnumerable<VisitorVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditVisitorVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditVisitorVM command);
        Task<OperationResult> UserRegistered(string code);
        Task<OperationResult> Create(CreateVisitorVM command);
    }
}
