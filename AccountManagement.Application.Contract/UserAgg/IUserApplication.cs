using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.UserAgg
{
    public interface IUserApplication
    {
        OperationResult Logout();
        Task<OperationResult> Login(LoginUserVM command);
        Task<OperationResult> Register(RegisterUserVM command);
    }
}
