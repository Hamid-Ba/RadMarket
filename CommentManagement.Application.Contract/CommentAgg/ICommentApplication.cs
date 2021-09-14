using Framework.Application;

namespace CommentManagement.Application.Contract.CommentAgg
{
    public interface ICommentApplication
    {
        OperationResult Delete(long id);
        OperationResult Confirm(long id);
        OperationResult DisConfirm(long id);
        OperationResult Create(CreateCommentVM command);
    }
}
