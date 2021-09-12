using Framework.Domain;

namespace AccountManagement.Domain.AdminUserAgg
{
    public class AdminUser : EntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }

        public AdminUser(string firstName, string lastName, string mobile, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;
            Password = password;
        }

        public void Edit(string firstName, string lastName, string mobile, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Mobile = mobile;

            if (!string.IsNullOrWhiteSpace(password))
                Password = password;
        }

    }
}
