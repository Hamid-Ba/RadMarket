using System.Collections.Generic;

namespace Framework.Application.Authentication
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public string Fullname { get; set; }
        public string Mobile { get; set; }
        public bool KeepMe { get; set; }

        public AuthViewModel()
        {
        }

        public AuthViewModel(long id, string fullname, string mobile,bool keepMe)
        {
            Id = id;
            Fullname = fullname;
            Mobile = mobile;
            KeepMe = keepMe;
        }
    }
}