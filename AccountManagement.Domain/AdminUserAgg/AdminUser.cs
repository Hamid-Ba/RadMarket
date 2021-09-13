using AccountManagement.Domain.AdminRoleAgg;
using Framework.Domain;

namespace AccountManagement.Domain.AdminUserAgg
{
    public class AdminUser : EntityBase
    {
        public long AdminRoleId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }

        public AdminRole Role { get; private set; }

        public AdminUser(long adminRoleId,string firstName, string lastName, string mobile, string password)
        {
            AdminRoleId = adminRoleId;
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;
            Password = password;
        }

        public void Edit(long adminRoleId,string firstName, string lastName, string mobile, string password)
        {
            AdminRoleId = adminRoleId;
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(password))
                Password = password;
        }

    }
}
