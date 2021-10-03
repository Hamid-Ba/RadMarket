using Framework.Infrastructure;
using TicketManagement.Domain.StoreTicketAgg;

namespace TicketManagement.Infrastructure.EfCore.Repository
{
    public class StoreTicketRepository : Repository<StoreTicket>, IStoreTicketRepository
    {
        private readonly TicketContext _context;

        public StoreTicketRepository(TicketContext context) : base(context) => _context = context;

    }
}