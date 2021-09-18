using BlogManagement.Application.Contract.ArticleCategoryAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.EfCore.Repository
{
    public class ArticleCategoryRepository : Repository<ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogContext _context;

        public ArticleCategoryRepository(BlogContext context) : base(context) => _context = context;

        public async Task<EditArticleCategoryVM> GetDetailForEditBy(long id) => await _context.ArticleCategories.Select(c =>
            new EditArticleCategoryVM()
            {
                Id = c.Id,
                Description = c.Description,
                Keywords = c.Keywords,
                MetaDescription = c.MetaDescription,
                Name = c.Name,
                Slug = c.Slug,
                ShowOrder = c.ShowOrder
            }).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

        public string GetCategorySlugBy(long id) => _context.ArticleCategories.FirstOrDefault(c => c.Id == id)?.Slug;
    }
}