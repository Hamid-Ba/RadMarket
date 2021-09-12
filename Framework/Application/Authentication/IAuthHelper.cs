using System.Threading.Tasks;

namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        void SignOut();
        void Signin(UserAuthViewModel account);
        long GetUserId();

    }
}