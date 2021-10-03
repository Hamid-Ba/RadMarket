using Framework.Domain;
using System.Collections.Generic;

namespace TicketManagement.Domain.StoreTicketAgg
{
    public class StoreTicket : EntityBase
    {
        public string Title { get; private set; }
        public long FirstStoreId { get; private set; }
        public long SecondStoreId { get; private set; }

        public List<StoreTicketMessage> Messages { get; private set; }

        public StoreTicket(string title, long firstStoreId, long secondStoreId)
        {
            Title = title;
            FirstStoreId = firstStoreId;
            SecondStoreId = secondStoreId;
            Messages = new List<StoreTicketMessage>();
        }

        public void AddMessage(StoreTicketMessage message) => Messages.Add(message);
    }
}
