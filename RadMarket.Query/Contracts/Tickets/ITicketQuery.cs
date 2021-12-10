using System.Collections.Generic;
using System.Threading.Tasks;

namespace RadMarket.Query.Contracts.Tickets
{
    public interface ITicketQuery
    {
        Task<IEnumerable<TicketQueryVM>> GetUserTicketsBy(long userId);
        Task<TicketQueryVM> GetTicketDetailBy(long ticketId, long userId);
    }
}