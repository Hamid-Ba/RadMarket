using Framework.Application;
using System.Threading.Tasks;

namespace CommentManagement.Application.Contract.CommentAgg
{
    public interface ICommentApplication
    {
        Task<OperationResult> Delete(long id);
        Task<OperationResult> Confirm(long id);
        Task<OperationResult> DisConfirm(long id);
        Task<OperationResult> Create(CreateCommentVM command);
    }
}
