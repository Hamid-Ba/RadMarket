using System.Collections.Generic;
using Framework.Application.TicketComponents;

namespace RadMarket.Query.Contracts.Tickets
{
    public class TicketQueryVM
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string Title { get; set; }
        public TicketSection Section { get; set; }
        public TicketStatus Status { get; set; }
        public TicketNecessary Necessary { get; set; }
        public string CreationDate { get; set; }
        public List<MessageQueryVM> Messages { get; set; }
    }

    public class MessageQueryVM
    {
        public long TicketId { get; set; }
        public long UserId { get; set; }
        public long SenderId { get; set; }
        public long ReciverId { get; set; }
        public string Text { get; set; }
        public string SentDate { get; set; }
    }
}
