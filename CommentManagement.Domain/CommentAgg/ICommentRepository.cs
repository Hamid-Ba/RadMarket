using CommentManagement.Application.Contract.CommentAgg;
using Framework.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<CommentVM>> GetAll(SearchCommentVM search);
    }
}
