using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.TicketAgg;
using TicketManagement.Domain.TicketAgg;

namespace TicketManagement.Infrastructure.EfCore.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private readonly TicketContext _context;
        private readonly StoreContext _storeContext;

        public TicketRepository(TicketContext context, StoreContext storeContext) : base(context)
        {
            _context = context;
            _storeContext = storeContext;
        }

        public async Task<IEnumerable<TicketVM>> GetAll()
        {
            var stores = await _storeContext.Stores.Select(s => new { Id = s.Id, Title = s.Name }).ToListAsync();

            var model = await _context.Tickets.Select(t => new TicketVM
            {
                Id = t.Id,
                UserId = t.UserId,
                Title = t.Title,
                Status = t.Status,
                Section = t.Section,
                Necessary = t.Necessary,
                IsReadByAdmin = t.IsReadByAdmin,
                IsReadByOwner = t.IsReadByOwner,
                CreationDate = t.CreationDate.ToFarsi()
            }).AsNoTracking().OrderByDescending(o => o.Id).ToListAsync();

            model.ForEach(t => t.StoreName = stores.Find(s => s.Id == t.UserId)?.Title);

            return model;
        }

        public async Task<TicketVM> GetMessages(long id) => await _context.Tickets.Select(t => new TicketVM
        {
            Id = t.Id,
            UserId = t.UserId,
            Messages = MapMessages(t.Messages)
        }).AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

        private static IEnumerable<MessagesVM> MapMessages(List<TicketMessage> messages) => messages.Select(m => new MessagesVM
        {
            Id = m.Id,
            TicketId = m.TicketId,
            SenderId = m.SenderId,
            ReciverId = m.ReciverId,
            Text = m.Text,
            CreationDate = m.CreationDate.ToFarsi()
        }).ToList();
        
    }
}