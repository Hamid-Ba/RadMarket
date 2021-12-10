using System;

namespace TicketManagement.Domain.TicketAgg
{
    public class TicketMessage
    {
        public long Id { get; private set; }
        public long TicketId { get; private set; }
        public long UserId { get; private set; }
        public string Text { get; private set; }
        public DateTime CreationDate { get; private set; }

        public Ticket Ticket { get; private set; }

        public TicketMessage(long ticketId, long userId, string text)
        {
            TicketId = ticketId;
            UserId = userId;
            Text = text;
            CreationDate = DateTime.Now;
        }
    }
}