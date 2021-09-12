using AccountManagement.Application.Contract.UserAgg;
using AccountManagement.Domain.UserAgg;
using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.Hashing;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserApplication(IAuthHelper authHelper, IPasswordHasher passwordHasher, IUserRepository userRepository)
        {
            _authHelper = authHelper;
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
        }

        public OperationResult Logout()
        {
            OperationResult result = new();

            _authHelper.SignOut();

            return result.Succeeded();
        }

        public async Task<OperationResult> Login(LoginUserVM command)
        {
            OperationResult result = new();

            var user = await _userRepository.GetUserBy(command.Mobile);

            if (user is null) return result.Failed(ApplicationMessage.UserNotExist);
            if (!user.IsActive) return result.Failed(ApplicationMessage.UserNotActive);

            var userAuthVm = new UserAuthViewModel(user.Id, $"{user.FirstName} {user.LastName}", user.Mobile, user.City, user.Province, user.Address, true);
            _authHelper.Signin(userAuthVm);

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

            return result.Succeeded();
        }
    }
}
