using Framework.Domain;
using System;

namespace StoreManagement.Domain.AdtPackageAgg
{
    public class AdtPackage : EntityBase
    {
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public AdtType Type { get; private set; }
        public double Cost { get; private set; }
        public string Description { get; private set; }
        public long OrderCount { get; private set; }

        public AdtPackage(string title, string imageName, AdtType type, double cost, string description)
        {
            Title = title;
            ImageName = imageName;
            Type = type;
            Cost = cost;
            Description = description;
            OrderCount = 0;
        }

        public void Edit(string title, string imageName, AdtType type, double cost, string description)
        {
            Title = title;

            if (!string.IsNullOrWhiteSpace(imageName))
                ImageName = imageName;

            Type = type;
            Cost = cost;
            Description = description;

            LastUpdateDate = DateTime.Now;
        }

        public long AddOrder() => ++OrderCount;
    }
}