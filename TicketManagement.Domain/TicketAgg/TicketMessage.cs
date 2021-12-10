using System;

namespace TicketManagement.Domain.TicketAgg
{
    public class TicketMessage
    {
        public long Id { get; private set; }
        public long TicketId { get; private set; }
        public long SenderId { get; private set; }
        public long ReciverId { get; private set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }

        public Ticket Ticket { get; private set; }

        public TicketMessage(long ticketId, long senderId,long reciverId, string text)
        {
            TicketId = ticketId;
            SenderId = senderId;
            ReciverId = reciverId;
            Text = text;
            CreationDate = DateTime.Now;
        }
    }
}