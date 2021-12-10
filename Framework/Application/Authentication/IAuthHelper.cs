namespace Framework.Application.Authentication
{
    public interface IAuthHelper
    {
        long GetUserId();
        long GetStoreId();
        void SignOut();
        void SignIn(StoreUserAuthVM account);
        void SignIn(AdminUserAuthVM account);
        void SignIn(UserAuthViewModel account);
    }
}