using Framework.Domain;
using System;

namespace StoreManagement.Domain.VisitorAgg
{
    public class Visitor : EntityBase
    {
        public string FullName { get; private set; }
        public string UniqueCode { get; private set; }
        public string Mobile { get; private set; }
        public long UserCount { get; set; }

        public Visitor(string fullName, string mobile)
        {
            FullName = fullName;
            UniqueCode = Guid.NewGuid().ToString().Substring(7);
            Mobile = mobile;
            UserCount = 0;
        }

        public void Edit(string fullName, string mobile)
        {
            FullName = fullName;
            Mobile = mobile;
        }

        public long UserRegistered() => ++UserCount;
    }
}
