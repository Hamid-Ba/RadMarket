using System.Collections.Generic;
using BlogManagement.Application.Contract.ArticleAgg;
using BlogManagement.Domain.ArticleAgg;
using Framework.Application;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleRepository : Repository<Article>, IArticleRepository
    {
        private readonly BlogContext _context;

        public ArticleRepository(BlogContext context) : base(context) => _context = context;

        public async Task<IEnumerable<ArticleVM>> GetAll() => await _context.Articles.Include(c => c.ArticleCategory)
            .Select(a => new ArticleVM()
            {
                Id = a.Id,
                ArticleCategoryId = a.ArticleCategoryId,
                ArticleCategoryName = a.ArticleCategory.Name,
                Title = a.Title,
                PictureName = a.PictureName,
                Author = a.Author,
                PublishDate = a.PublishDate.ToFarsi(),
                CreationDate = a.CreationDate.ToFarsi(),
                ShortDescription = a.ShortDescription.Substring(0,25)
            }).AsNoTracking().ToListAsync();

        public async Task<EditArticleVM> GetDetailForEditBy(long id) => await _context.Articles.Select(a => new EditArticleVM
        {
            Id = a.Id,
            ArticleCategoryId = a.ArticleCategoryId,
            Title = a.Title,
            Author = a.Author,
            ShortDescription = a.ShortDescription,
            PublishDate = a.PublishDate.ToFarsi(),
            Description = a.Description,
            PictureName = a.PictureName,
            PictureTitle = a.PictureTitle,
            PictureAlt = a.PictureAlt,
            Slug = a.Slug,
            Keywords = a.Keywords,
            MetaDescription = a.MetaDescription
        }).FirstOrDefaultAsync(a => a.Id == id);

    }
}