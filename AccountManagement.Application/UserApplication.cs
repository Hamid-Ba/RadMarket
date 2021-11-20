using AccountManagement.Application.Contract.UserAgg;
using AccountManagement.Domain.UserAgg;
using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using Framework.Application.SMS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly ISmsService _smsService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserApplication(IAuthHelper authHelper, ISmsService smsService, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _authHelper = authHelper;
            _smsService = smsService;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public OperationResult Logout()
        {
            OperationResult result = new();

            _authHelper.SignOut();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(id);
            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);

            user.Delete();

            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<EditUserVM> GetDetailForEditBy(long id) => await _userRepository.GetDetailForEditBy(id);

        public async Task<OperationResult> Edit(EditUserVM command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetEntityByIdAsync(command.Id);

            if (user is null) return result.Failed(ApplicationMessage.NotExist);

            string newPassword = null;

            if (!string.IsNullOrWhiteSpace(command.Password))
                if (command.Password == command.ConfirmPassword) newPassword = _passwordHasher.Hash(command.Password);

            user.Edit(command.FirstName, command.LastName, newPassword, command.City, command.Province, command.Address);
            await _userRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Login(LoginUserVM command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetUserBy(command.Mobile);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);
            if (!user.IsActive) return result.Failed(ApplicationMessage.UserNotActive);

            var userAuthVm = new UserAuthViewModel(user.Id, $"{user.FirstName} {user.LastName}", user.Mobile, user.City, user.Province, user.Address, true);
            _authHelper.SignIn(userAuthVm);

            return result.Succeeded();
        }

        public async Task<OperationResult> Register(RegisterUserVM command)
        {
            OperationResult result = new();

            if (_userRepository.Exists(u => u.Mobile == command.Mobile)) return result.Failed(ApplicationMessage.DuplicatedMobile);

            var password = _passwordHasher.Hash(command.Password);

            var user = new User(command.FirstName, command.LastName, command.MarketerCode, command.Mobile, password, command.City, command.Province, command.Address);

            await _userRepository.AddEntityAsync(user);
            await _userRepository.SaveChangesAsync();

            _smsService.SendSms(user.Mobile, $"{user.FirstName} {user.LastName} عزیز ، کد فعال سازی شما : {user.ActivationCode}  می باشد");

            return result.Succeeded();
        }

        public async Task<OperationResult> ActiveAccount(ActiveAccountUserVM command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetUserBy(command.Mobile);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);
            if (user.ActivationCode != command.ActivationCode) return result.Failed("کد فعال سازی درست نمی باشد");

            user.ActiveAccount();
            user.ReActivateCode(Guid.NewGuid().ToString().Substring(0, 7));

            await _userRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<IEnumerable<UserVM>> GetAll() => await _userRepository.GetAll();

        public async Task<AddressUserVM> GetAddressInfoBy(long id) => await _userRepository.GetAddressInfoBy(id);

        public async Task<string> GetUserFullNameBy(long id)
        {
            var user = await _userRepository.GetEntityByIdAsync(id);

            if (user is null) return "";

            return $"{user.FirstName} {user.LastName}";
        }

        public async Task<IEnumerable<UserVM>> GetAllBy(string marketerCode) => await _userRepository.GetAllBy(marketerCode);
        
    }
}
