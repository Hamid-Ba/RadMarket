using System.Collections.Generic;

namespace Framework.Application.Authentication
{
    public class UserAuthViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Address { get; set; }
        public bool KeepMe { get; set; }

        public UserAuthViewModel()
        {
        }

        public UserAuthViewModel(long id, string fullname, string mobile, string city, string province, string address, bool keepMe)
        {
            Id = id;
            Fullname = fullname;
            Mobile = mobile;
            City = city;
            Province = province;
            Address = address;
            KeepMe = keepMe;
        }
    }
}