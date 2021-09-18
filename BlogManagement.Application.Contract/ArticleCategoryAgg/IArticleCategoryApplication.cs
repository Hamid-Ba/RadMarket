﻿using System.Threading.Tasks;
using Framework.Application;

namespace BlogManagement.Application.Contract.ArticleCategoryAgg
{
    public interface IArticleCategoryApplication
    {
        Task<OperationResult> Delete(long id);
        Task<EditArticleCategoryVM> GetDetailForEditBy(long id);
        Task<OperationResult> Edit(EditArticleCategoryVM command);
        Task<OperationResult> Create(CreateArticleCategoryVM command);
    }
}