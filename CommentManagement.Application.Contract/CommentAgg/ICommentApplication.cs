using Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentManagement.Application.Contract.CommentAgg
{
    public interface ICommentApplication
    {
        Task<OperationResult> Delete(long id);
        Task<OperationResult> Create(CreateCommentVM command);
        Task<IEnumerable<CommentVM>> GetAll(SearchCommentVM search);
    }
}
