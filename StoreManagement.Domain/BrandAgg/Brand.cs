using Framework.Domain;
using StoreManagement.Domain.ProductAgg;
using StoreManagement.Domain.StoreAgg;
using System;
using System.Collections.Generic;

namespace StoreManagement.Domain.BrandAgg
{
    public class Brand : EntityBase
    {
        public long StoreId { get; private set; }
        public string Name { get; private set; }

        public Store  Store { get; private set; }
        public List<Product> Products { get; private set; }

        public Brand(long storeId, string name)
        {
            StoreId = storeId;
            Name = name;
        }

        public void Edit(string name)
        {
            Name = name;
            LastUpdateDate = DateTime.Now;
        }

    }
}