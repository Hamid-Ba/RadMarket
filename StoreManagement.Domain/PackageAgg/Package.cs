using Framework.Domain;
using System;

namespace StoreManagement.Domain.PackageAgg
{
    public class Package : EntityBase
    {
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public int PackagesCount { get; private set; }
        public double Cost { get; private set; }
        public string Description { get; private set; }
        public long OrderCount { get; private set; }


        public Package(string title, string imageName,int packagesCount, double cost, string description)
        {
            Title = title;
            ImageName = imageName;
            PackagesCount = packagesCount;
            Cost = cost;
            Description = description;
            OrderCount = 0;
        }

        public void Edit(string title, string imageName,int packagesCount, double cost, string description)
        {
            Title = title;

            if (!string.IsNullOrWhiteSpace(imageName))
                ImageName = imageName;

            PackagesCount = packagesCount;
            Cost = cost;
            Description = description;

            LastUpdateDate = DateTime.Now;
        }

        public long AddOrder() => ++OrderCount;
    }
}