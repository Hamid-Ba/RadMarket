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
        public string AccountNumber { get; private set; }
        public string ShabaNumber { get; private set; }
        public string CardNumber { get; private set; }
        public string PhoneNumber { get; private set; }
        public string MobileNumber { get; private set; }
        public StoreStatus Status { get; private set; }
        public string Description { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string Address { get; private set; }
        public string StoreGivenStatusReason { get; private set; }

        public List<Product> Products { get; private set; }

        public Store(long storeAdminUserId,string name,string mobile, string province, string city,string address)
        {
            StoreAdminUserId = storeAdminUserId;
            UniqueCode = Guid.NewGuid().ToString().Substring(0, 8);
            Name = name;
            MobileNumber = mobile;
            Province = province;
            City = city;
            Address = address;
        }

        public Store(long storeAdminUserId, string name,string accountNumber,string shabaNumber,string cardNumber,string phoneNumber, string mobileNumber, string province, string city, string address, StoreStatus status)
        {
            StoreAdminUserId = storeAdminUserId;
            UniqueCode = Guid.NewGuid().ToString().Substring(0, 8);
            Name = name;
            AccountNumber = accountNumber;
            ShabaNumber = "IR"+shabaNumber;
            CardNumber = cardNumber;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            Status = status;
            City = city;
            Province = province;
            Address = address;
            StoreGivenStatusReason = "شرکت تازه ثبت شده";
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