using Framework.Domain;
using System;

namespace StoreManagement.Domain.StoreAgg
{
    public class Store : EntityBase
    {
        public long AdminUserId { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string MobileNumber { get; private set; }
        public StoreStatus Status { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string StoreGivenStatusReason { get; private set; }

        public Store(long adminUserId, string name,string phoneNumber, string mobileNumber, StoreStatus status, string address)
        {
            AdminUserId = adminUserId;
            Name = name;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            Status = status;
            Address = address;
            StoreGivenStatusReason = "";
        }

        public void Edit(string name,string phoneNumber, string mobileNumber, string description, string address)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            Description = description;
            Address = address;
            LastUpdateDate = DateTime.Now;
        }

        public void ChangeStatus(StoreStatus status, string reason)
        {
            Status = status;

            if (!string.IsNullOrWhiteSpace(reason)) StoreGivenStatusReason = reason;

            LastUpdateDate = DateTime.Now;
        }
    }
}