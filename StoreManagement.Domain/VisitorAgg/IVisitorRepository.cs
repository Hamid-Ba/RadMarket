using Framework.Domain;
using StoreManagement.Application.Contract.VisitorAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreManagement.Domain.VisitorAgg
{
    public interface IVisitorRepository : IRepository<Visitor>
    {
        Task<Visitor> GetBy(string code);
        Task<IEnumerable<VisitorVM>> GetAll();
        Task<EditVisitorVM> GetDetailForEditBy(long id);
    }
}
