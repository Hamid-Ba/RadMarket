using Framework.Domain;
using System;

namespace AdminManagement.Domain.ProvinceAgg
{
    public class Province : EntityBase
    {
        public string Name { get; private set; }

        public Province(string name)
        {
            Name = name;
        }

        public void Edit(string name)
        {
            Name = name;
            LastUpdateDate = DateTime.Now;
        }
    }
}
