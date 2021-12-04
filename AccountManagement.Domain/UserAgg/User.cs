using System;
using Framework.Domain;
namespace AccountManagement.Domain.UserAgg
{
    public class User : EntityBase
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string MarketerCode { get; private set; }
        public string Mobile { get; private set; }
        public string Password { get; private set; }
        public string City { get; private set; }
        public string Province { get; private set; }
        public string Address { get; private set; }
        public string ActivationCode { get; private set; }
        public bool IsActive { get; private set; }

        public User(string firstName, string lastName, string marketerCode, string mobile, string password, string city, string province, string address)
        {
            FirstName = firstName;
            LastName = lastName;
            MarketerCode = marketerCode;
            Mobile = mobile;
            Password = password;
            City = city;
            Province = province;
            Address = address;
            ActivationCode = Guid.NewGuid().ToString().Substring(0, 7);
        }

        public void Edit(string firstName, string lastName, string password, string city, string province, string address)
        {
            FirstName = firstName;
            LastName = lastName;

            if (!string.IsNullOrWhiteSpace(password))
                Password = password;

            City = city;
            Province = province;
            Address = address;
        }

        public void ChangePassword(string newPassword) => Password = newPassword;

        public void ActiveAccount() => IsActive = true;

        public void ReActivateCode(string newActivationCode) => ActivationCode = newActivationCode;
    }
}
