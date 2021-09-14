using CommentManagement.Application.Contract.CommentAgg;
using CommentManagement.Domain.CommentAgg;
using Framework.Application;
using System.Threading.Tasks;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository) => _commentRepository = commentRepository;

        public async Task<OperationResult> Create(CreateCommentVM command)
        {
            OperationResult result = new OperationResult();

            var comment = new Comment(command.StoreId, command.Name, command.Mobile, command.Message, command.Type, command.OwnerId,
                command.OwnerName, command.ParentId);

            await _commentRepository.AddEntityAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Delete(long id)
        {
            OperationResult result = new OperationResult();

            var comment = await _commentRepository.GetEntityByIdAsync(id);

            if (comment == null) return result.Failed(ApplicationMessage.NotExist);

            comment.Delete();
            await _commentRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> Confirm(long id)
        {
            OperationResult result = new OperationResult();

            var comment = await _commentRepository.GetEntityByIdAsync(id);
            if (comment is null) return result.Failed(ApplicationMessage.NotExist);

            comment.ConfirmedComment();
            await _commentRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> DisConfirm(long id)
        {
            OperationResult result = new OperationResult();

            var comment = await _commentRepository.GetEntityByIdAsync(id);
            if (comment is null) return result.Failed(ApplicationMessage.NotExist);

            comment.DisConfirmedComment();
            await _commentRepository.SaveChangesAsync();

            return result.Succeeded();
        }

    }
}