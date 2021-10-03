using System;

namespace TicketManagement.Domain.StoreTicketAgg
{
    public class StoreTicketMessage
    {
        public long Id { get; private set; }
        public long StoreTicketId { get; private set; }
        public long SenderId { get; private set; }
        public long ReciverId { get; private set; }
        public string Message { get; private set; }
        public DateTime SentDate { get; private set; }

        public StoreTicket StoreTicket { get; private set; }

        public StoreTicketMessage(long storeTicketId, long senderId, long reciverId, string message)
        {
            StoreTicketId = storeTicketId;
            SenderId = senderId;
            ReciverId = reciverId;
            Message = message;
            SentDate = DateTime.Now;
        }
    }
}
