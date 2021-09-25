using System.Collections.Generic;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.CategoryAgg;
using StoreManagement.Domain.CategoryAgg;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly StoreContext _context;

        public CategoryRepository(StoreContext context) : base(context) => _context = context;

        public async Task<IEnumerable<CategoryVM>> GetAll() => await _context.Categories.Select(c => new CategoryVM()
        {
            Id = c.Id,
            Name = c.Name,
            Description = c.Description,
            Slug = c.Slug
        }).AsNoTracking().ToListAsync();

        public async Task<EditCategoryVM> GetDetailForEditBy(long id) => await _context.Categories.Select(c => new EditCategoryVM
        {
            Id = c.Id,
            Name = c.Name,
            Slug = c.Slug,
            KeyWords = c.KeyWords,
            Description = c.Description,
            MetaDescription = c.MetaDescription
        }).FirstOrDefaultAsync(c => c.Id == id);

    }
}