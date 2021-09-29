using BlogManagement.Infrastructure.EfCore;
using Framework.Application;
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
            .Where(a => a.PublishDate <= DateTime.Now && !a.IsDelete)
            .Select(a => new ArticleQueryVM
        {
            Id = a.Id,
            PictureName = a.PictureName,
            PictureAlt = a.PictureAlt,
            PictureTitle = a.PictureTitle,
            Title = a.Title,
            Slug = a.Slug,
            ShortDescription = a.ShortDescription.Substring(50) + " ..."
        }).AsNoTracking().OrderByDescending(a => a.Id).ToListAsync();

        public async Task<ArticleQueryVM> GetBy(string slug) => await _blogContext.Articles.Include(a => a.ArticleCategory)
            .Where(a => a.PublishDate <= DateTime.Now && !a.IsDelete)
            .Select(a => new ArticleQueryVM
            {
                Id = a.Id,
                CategoryId = a.ArticleCategoryId,
                CategoryName = a.ArticleCategory.Name,
                PictureName = a.PictureName,
                PictureAlt = a.PictureAlt,
                PictureTitle = a.PictureTitle,
                Title = a.Title,
                Slug = a.Slug,
                ShortDescription = a.ShortDescription,
                Author = a.Author,
                Description = a.Description,
                PublishDate = a.PublishDate.ToFarsi(),
                Keywords = a.Keywords,
                MetaDescription = a.MetaDescription
            }).FirstOrDefaultAsync(a => a.Slug == slug);

        public async Task<IEnumerable<ArticleQueryVM>> GetForSideBar(int take = 5) => await _blogContext.Articles.Include(a => a.ArticleCategory)
            .Where(a => a.PublishDate <= DateTime.Now && !a.IsDelete)
            .Select(a => new ArticleQueryVM
            {
                Id = a.Id,
                Title = a.Title,
                Slug = a.Slug,
                ShortDescription = a.ShortDescription.Substring(0,25) + " ..."
                
            }).AsNoTracking().OrderByDescending(a => a.Id).ToListAsync();

    }
}