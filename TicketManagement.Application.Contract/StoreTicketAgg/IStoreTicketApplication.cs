using Framework.Application;
using System.Threading.Tasks;

namespace TicketManagement.Application.Contract.StoreTicketAgg
{
    public interface IStoreTicketApplication
    {
        Task<OperationResult> Create(CreateStoreTicketVM command);
        Task<OperationResult> SendMessage(AddStoreTicketMessageVM command);
    }
}
