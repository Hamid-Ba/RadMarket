using Framework.Domain;
using StoreManagement.Domain.PackageOrderAgg;
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
        //Package Properties
        public int ChargeCount { get; private set; }
        public DateTime ChargeExpiredDate { get; private set; }
        public int ProductsCount { get; private set; }
        //Adt Package Properties
        public int AdtChargeCount { get; private set; }
        public DateTime AdtChargeExpiredDate { get; private set; }

        public List<Product> Products { get; private set; }
        public List<PackageOrder> PackageOrders { get; private set; }

        public Store(long storeAdminUserId, string name, string mobile, string province, string city, string address)
        {
            StoreAdminUserId = storeAdminUserId;
            UniqueCode = Guid.NewGuid().ToString().Substring(0, 8);
            Name = name;
            MobileNumber = mobile;
            Province = province;
            City = city;
            Address = address;
            ChargeCount = 0;
            ProductsCount = 0;
            AdtChargeCount = 0;
        }

        public Store(long storeAdminUserId, string name, string accountNumber, string shabaNumber, string cardNumber, string phoneNumber,
            string mobileNumber, string province, string city, string address, StoreStatus status)
        {
            StoreAdminUserId = storeAdminUserId;
            UniqueCode = Guid.NewGuid().ToString().Substring(0, 8);
            Name = name;
            AccountNumber = accountNumber;
            ShabaNumber = "IR" + shabaNumber;
            CardNumber = cardNumber;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            Status = status;
            City = city;
            Province = province;
            Address = address;
            StoreGivenStatusReason = "شرکت تازه ثبت شده";
            ChargeCount = 0;
            ProductsCount = 0;
            AdtChargeCount = 0;
        }

        public void Edit(string name, string phoneNumber, string mobileNumber, string accountNumber, string shabaNumber, string cardNumber
            , string description, string province, string city, string address)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            MobileNumber = mobileNumber;
            AccountNumber = accountNumber;
            ShabaNumber = shabaNumber;
            CardNumber = cardNumber;
            Description = description;
            Province = province;
            City = city;
            Address = address;
            LastUpdateDate = DateTime.Now;
        }

        public void ChangeStatus(StoreStatus status, string reason)
        {
            Status = status;

            if (!string.IsNullOrWhiteSpace(reason)) StoreGivenStatusReason = reason;

            LastUpdateDate = DateTime.Now;
        }

        public bool IsAccountStillCharged()
        {
            if (ChargeExpiredDate >= DateTime.Now) return true;
            return false;
        }

        public bool IsAccountStillAdtCharged()
        {
            if (AdtChargeExpiredDate >= DateTime.Now) return true;
            return false;
        }

        public bool IsAbleToAddProduct() => ProductsCount > 0 ? true : false;

        public int ProductCreated() => --ProductsCount;

        public void Charge(int period, int productCount)
        {
            ChargeCount++;
            ProductsCount = productCount;
            ChargeExpiredDate = DateTime.Now.AddDays(period);
        }

        public void ChargeAdt(int period)
        {
            AdtChargeCount++;
            AdtChargeExpiredDate = DateTime.Now.AddDays(period);
        }
    }
}