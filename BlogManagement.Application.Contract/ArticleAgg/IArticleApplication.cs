using System.Threading.Tasks;
using Framework.Application;

namespace BlogManagement.Application.Contract.ArticleAgg
{
    public interface IArticleApplication
    {
        Task<OperationResult> Delete(long id);
        Task<EditArticleVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditArticleVM command);
        Task<OperationResult> Create(CreateArticleVM command);
    }
}