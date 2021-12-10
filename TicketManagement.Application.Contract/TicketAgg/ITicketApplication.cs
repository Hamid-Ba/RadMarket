using System.Collections.Generic;
using System.Threading.Tasks;
using Framework.Application;

namespace TicketManagement.Application.Contract.TicketAgg
{
    public interface ITicketApplication
    {
        Task<TicketVM> GetMessages(long id);
        Task<IEnumerable<TicketVM>> GetAll();
        Task<OperationResult> OpenOrClose(long id, bool close);
        Task<OperationResult> Create(CreateTicketVM command);
        Task<OperationResult> AddMessage(AddMessageTicketVM command);
        Task<OperationResult> SendResponse(AddMessageTicketVM command);
    }
}