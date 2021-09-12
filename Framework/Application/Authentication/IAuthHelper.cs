using System.Threading.Tasks;

namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        void SignOut();
        void SignIn(AdminUserAuthVM account);
        void SignIn(UserAuthViewModel account);
    }
}