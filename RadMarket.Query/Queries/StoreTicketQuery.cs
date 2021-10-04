using Framework.Application;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.StoreTicketAgg;
using StoreManagement.Infrastructure.EfCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketManagement.Domain.StoreTicketAgg;
using TicketManagement.Infrastructure.EfCore;

namespace RadMarket.Query.Queries
{
    public class StoreTicketQuery : IStoreTicketQuery
    {
        private readonly StoreContext _storeContext;
        private readonly TicketContext _ticketContext;

        public StoreTicketQuery(StoreContext storeContext, TicketContext ticketContext)
        {
            _storeContext = storeContext;
            _ticketContext = ticketContext;
        }

        public async Task<IEnumerable<StoreTicketQueryVM>> GetAllBy(long storeId)
        {
            var stores = await _storeContext.Stores.Select(s => new { Id = s.Id, Name = s.Name }).ToListAsync();

            var result = await _ticketContext.StoreTickets
            .Where(t => t.FirstStoreId == storeId || t.SecondStoreId == storeId)
            .Select(t => new StoreTicketQueryVM
            {
                Id = t.Id,
                Title = t.Title,
                FirstStoreId = t.FirstStoreId,
                SecondStoreId = t.SecondStoreId,
                CreationDate = t.CreationDate.ToFarsi(),
                GeorgianCreationDate = t.CreationDate,
                Messages = MapMessages(t.Messages)
            }).AsNoTracking().OrderByDescending(t => t.GeorgianCreationDate).ToListAsync();

            result.ForEach(t => t.FirstStoreName = stores.FirstOrDefault(s => s.Id == t.FirstStoreId)?.Name);
            result.ForEach(t => t.SecondStoreName = stores.FirstOrDefault(s => s.Id == t.SecondStoreId)?.Name);

            return result;
        }

        private static List<StoreTicketMessageQueryVM> MapMessages(List<StoreTicketMessage> messages) => messages.Select(m => new StoreTicketMessageQueryVM
        {
            Id = m.Id,
            StoreTicketId = m.StoreTicketId,
            ReciverId = m.ReciverId,
            SenderId = m.SenderId,
            SentDate = m.SentDate.ToFarsiFull(),
            GeorgianSentDate = m.SentDate,
            Message = m.Message
        }).ToList();

    }
}