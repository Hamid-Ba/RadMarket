using Framework.Application;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contract.UserAgg
{
    public interface IUserApplication
    {
        Task<OperationResult> Register(RegisterUserVM command);
    }
}
