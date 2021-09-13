﻿using System;
using Framework.Domain;

namespace AccountManagement.Domain.StoreUserAgg
{
    public class StoreUser : EntityBase
    {
        public long StoreId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string Address { get; private set; }
        public bool IsActive { get; private set; }


        public StoreUser(long storeId, string firstName, string lastName, string mobile, string password, string city, string province, string address)
        {
            StoreId = storeId;
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;
            Password = password;
            City = city;
            Province = province;
            Address = address;
            IsActive = false;
        }

        public void Edit(string firstName, string lastName, string mobile, string password, string city, string province, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(mobile))
                Password = password;

            City = city;
            Province = province;
            Address = address;

            LastUpdateDate = DateTime.Now;
        }

        public void FillStoreId(long storeId)
        {
            StoreId = storeId;
            LastUpdateDate = DateTime.Now;
        }

        public void ActiveAccount()
        {
            IsActive = true;
            LastUpdateDate = DateTime.Now;
        }

    }
}