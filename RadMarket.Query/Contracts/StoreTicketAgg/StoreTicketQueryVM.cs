using System;
using System.Collections.Generic;

namespace RadMarket.Query.Contracts.StoreTicketAgg
{
    public class StoreTicketQueryVM
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public long FirstStoreId { get; set; }
        public string FirstStoreName { get; set; }
        public long SecondStoreId { get; set; }
        public string SecondStoreName { get; set; }
        public string CreationDate { get; set; }
        public DateTime GeorgianCreationDate { get; set; }

        public List<StoreTicketMessageQueryVM> Messages { get; set; }
    }

    public class StoreTicketMessageQueryVM
    {
        public long Id { get; set; }
        public long StoreTicketId { get; set; }
        public long SenderId { get; set; }
        public long ReciverId { get; set; }
        public string Message { get; set; }
        public string SentDate { get; set; }
        public DateTime GeorgianSentDate { get; set; }
    }
}
