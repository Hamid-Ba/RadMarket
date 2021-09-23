﻿using System.Collections.Generic;
using Framework.Domain;
using StoreManagement.Application.Contract.StoreAgg;
using System.Threading.Tasks;

namespace StoreManagement.Domain.StoreAgg
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<string> GetStoreCode(long id);
        Task<IEnumerable<StoreVM>> GetAll();
        Task<Store> GetStoreBy(string code);
        Task<EditStoreVM> GetDetailForEditBy(long id);
    }
}
