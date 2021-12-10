using System.Threading.Tasks;
using Framework.Application;

namespace TicketManagement.Application.Contract.TicketAgg
{
    public interface ITicketApplication
    {
        Task<OperationResult> Create(CreateTicketVM command);
        Task<OperationResult> AddMessage(AddMessageTicketVM command);
    }
}