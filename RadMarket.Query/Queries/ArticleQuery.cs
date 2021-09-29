using BlogManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.ArticleAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleQuery(BlogContext blogContext) => _blogContext = blogContext;

        public async Task<IEnumerable<ArticleQueryVM>> GetAll() => await _blogContext.Articles.Include(a => a.ArticleCategory)
            .Where(a => a.PublishDate >= DateTime.Now && !a.IsDelete)
            .Select(a => new ArticleQueryVM
        {
            Id = a.Id,
            PictureName = a.PictureName,
            PictureAlt = a.PictureAlt,
            PictureTitle = a.PictureTitle,
            Title = a.Title,
            ShortDescription = a.ShortDescription.Substring(50) + " ..."
        }).AsNoTracking().OrderByDescending(a => a.Id).ToListAsync();
    }
}