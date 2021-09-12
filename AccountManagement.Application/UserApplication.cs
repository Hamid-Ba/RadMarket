using AccountManagement.Application.Contract.UserAgg;
using AccountManagement.Domain.UserAgg;
using Framework.Application;
using Framework.Application.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class UserApplication : IUserApplication
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;

        public UserApplication(IPasswordHasher passwordHasher,IUserRepository userRepository) 
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
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
