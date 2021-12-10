using Framework.Infrastructure;
using TicketManagement.Domain.TicketAgg;

namespace TicketManagement.Infrastructure.EfCore.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly TicketContext _context;

        public TicketRepository(TicketContext context) : base(context) => _context = context;
    }
}
