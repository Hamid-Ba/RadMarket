using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommentManagement.Application.Contract.CommentAgg;
using CommentManagement.Domain.CommentAgg;
using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CommentManagement.Infrastructure.EfCore.Repository
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        private readonly CommentContext _context;

        public CommentRepository(CommentContext context) : base(context) => _context = context;

        public async Task<IEnumerable<CommentVM>> GetAll(SearchCommentVM search)
        {
            var query = _context.Comments.Select(c => new CommentVM()
            {
                Id = c.Id,
                StoreId = c.StoreId,
                Name = c.Name,
                Mobile = c.Mobile,
                Score = c.Score,
                OwnerId = c.OwnerId,
                Type = c.Type,
                OwnerName = c.OwnerName,
            }).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(search.Name)) query = query.Where(c => c.Name.Contains(search.Name));
            if (!string.IsNullOrWhiteSpace(search.Mobile)) query = query.Where(c => c.Mobile.Contains(search.Mobile));

            return await query.ToListAsync();
        }
    }
}
