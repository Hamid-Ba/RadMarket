using System.Collections.Generic;
using BlogManagement.Domain.ArticleAgg;
using Framework.Domain;
using System;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }

        public List<Article> Articles { get; private set; }

        public ArticleCategory(string name, string description, int showOrder, string slug, string keywords, string metaDescription)
        {
            Name = name;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public void Edit(string name, string description,int showOrder, string slug, string keywords, string metaDescription)
        {
            Name = name;
            Description = description;
            ShowOrder = showOrder;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;

            LastUpdateDate = DateTime.Now;
        }
    }
}