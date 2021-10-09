using System;
using Framework.Application;
using Framework.Domain;
using StoreManagement.Domain.CategoryAgg;
using StoreManagement.Domain.StoreAgg;

namespace StoreManagement.Domain.ProductAgg
{
    public class Product : EntityBase
    {
        public long StoreId { get; private set; }
        public long CategoryId { get; private set; }
        public string Code { get; private set; }
        public long OrderCount { get; private set; }
        public string Name { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public int EachBoxCount { get; private set; }
        public double ConsumerPrice { get; private set; }
        public double PurchacePrice { get; private set; }
        public double MoneyGain { get; private set; }
        public int Stock { get; private set; }
        public int Prize { get; private set; }
        public string Description { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string ProductAcceptOrRejectDescription { get; private set; }
        public ProductAcceptanceState ProductAcceptanceState { get; private set; }

        public Store Store { get; private set; }
        public Category Category { get; private set; }

        public Product(long storeId, long categoryId,string code, string name, string picture, string pictureAlt, string pictureTitle, int eachBoxCount,
            double consumerPrice, double purchacePrice, int stock, int prize, string description, string slug,
            string keywords, string metaDescription, string productAcceptOrRejectDescription,
            ProductAcceptanceState productAcceptanceState)
        {
            StoreId = storeId;
            CategoryId = categoryId;
            Code = code;
            OrderCount = 0;
            Name = name;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            EachBoxCount = eachBoxCount;
            ConsumerPrice = consumerPrice;
            PurchacePrice = purchacePrice;
            MoneyGain = ConsumerPrice - PurchacePrice;
            Stock = stock;
            Prize = prize;
            Description = description;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            ProductAcceptOrRejectDescription = productAcceptOrRejectDescription;
            ProductAcceptanceState = productAcceptanceState;
        }

        public void Edit(long categoryId,string code, string name, string picture, string pictureAlt, string pictureTitle, int eachBoxCount,
            double consumerPrice, double purchacePrice, int stock, int prize, string description, string slug,
            string keywords, string metaDescription)
        {
            CategoryId = categoryId;
            Code = code;
            Name = name;

            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;

            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            EachBoxCount = eachBoxCount;
            ConsumerPrice = consumerPrice;
            PurchacePrice = purchacePrice;
            MoneyGain = ConsumerPrice - PurchacePrice;
            Stock = stock;
            Prize = prize;
            Description = description;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;

            LastUpdateDate = DateTime.Now;
        }

        public void SetProductState(ProductAcceptanceState state, string reason)
        {
            ProductAcceptanceState = state;
            ProductAcceptOrRejectDescription = reason;

            LastUpdateDate = DateTime.Now;
        }

        public void AddOrder() => ++OrderCount;
    }
}