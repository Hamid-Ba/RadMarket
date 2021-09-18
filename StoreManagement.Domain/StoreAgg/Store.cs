using Framework.Domain;
using StoreManagement.Domain.ProductAgg;
using System;
using System.Collections.Generic;

namespace StoreManagement.Domain.StoreAgg
{
    public class Store : EntityBase
    {
        public long StoreAdminUserId { get; private set; }
        public string UniqueCode { get; private set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string MobileNumber { get; private set; }
        public StoreStatus Status { get; private set; }
        public string Description { get; private set; }
        public string Address { get; private set; }
        public string StoreGivenStatusReason { get; private set; }

        public List<Product> Products { get; private set; }

        public Store(long storeAdminUserId,string mobile)
        {
            StoreAdminUserId = storeAdminUserId;
            UniqueCode = Guid.NewGuid().ToString().Substring(0, 8);
            MobileNumber = mobile;
        }

        public Store(long storeAdminUserId, string name,string phoneNumber, string mobileNumber, StoreStatus status, string address)
        {
            StoreAdminUserId = storeAdminUserId;
            UniqueCode = Guid.NewGuid().ToString().Substring(0, 8);
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