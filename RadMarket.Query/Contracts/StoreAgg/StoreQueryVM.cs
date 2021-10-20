using Framework.Domain;
using System;

namespace RadMarket.Query.Contracts.StoreAgg
{
    public class StoreQueryVM
    {
        public long Id { get; set; }
        public long StoreAdminUserId { get;  set; }
        public string UniqueCode { get;  set; }
        public string Name { get;  set; }
        public string AccountNumber { get;  set; }
        public string ShabaNumber { get;  set; }
        public string CardNumber { get;  set; }
        public string PhoneNumber { get;  set; }
        public string MobileNumber { get;  set; }
        public StoreStatus Status { get;  set; }
        public string Description { get;  set; }
        public string City { get;  set; }
        public string Province { get;  set; }
        public string Address { get;  set; }
        public string StoreGivenStatusReason { get;  set; }
        //Package Properties
        public int ChargeCount { get;  set; }
        public DateTime ChargeExpiredDate { get;  set; }
        public int ProductsCount { get;  set; }
        //Adt Package Properties
        public int AdtChargeCount { get;  set; }
        public DateTime AdtChargeExpiredDate { get;  set; }
    }
}