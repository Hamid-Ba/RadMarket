﻿using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.UserAgg
{
    public interface IUserApplication
    {
        OperationResult Logout();
        Task<IEnumerable<UserVM>> GetAll();
        Task<OperationResult> Delete(long id);
        Task<EditUserVM> GetDetailForEditBy(long id);
        Task<AddressUserVM> GetAddressInfoBy(long id);
        Task<OperationResult> Edit(EditUserVM command);
        Task<OperationResult> Login(LoginUserVM command);
        Task<OperationResult> Register(RegisterUserVM command);
        Task<OperationResult> ActiveAccount(ActiveAccountUserVM command);
    }
}
