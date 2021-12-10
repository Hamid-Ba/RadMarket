using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.TicketAgg;

namespace TicketManagement.Domain.TicketAgg
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        Task<TicketVM> GetMessages(long id);
        Task<IEnumerable<TicketVM>> GetAll();
    }
}
