﻿using BlogManagement.Application;
using BlogManagement.Application.Contract.ArticleAgg;
using BlogManagement.Application.Contract.ArticleCategoryAgg;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EfCore;
using BlogManagement.Infrastructure.EfCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RadMarket.Query.Contracts.ArticleAgg;
using RadMarket.Query.Queries;

namespace BlogManagement.Infrastructure.Configuration
{
    public class BlogManagementBootstrapper
    {
        public static void Configuration(IServiceCollection service, string connectionString)
        {
            #region Config Context
            service.AddDbContext<BlogContext>(options => options.UseSqlServer(connectionString));
            #endregion

            service.AddTransient<IArticleRepository, ArticleRepository>();
            service.AddTransient<IArticleApplication, ArticleApplication>();

            service.AddTransient<IArticleCategoryRepository, ArticleCategoryRepository>();
            service.AddTransient<IArticleCategoryApplication, ArticleCategoryApplication>();

            #region Query

            service.AddTransient<IArticleQuery, ArticleQuery>();

            #endregion
        }
    }
}
