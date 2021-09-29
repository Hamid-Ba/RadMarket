using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;
using BlogManagement.Infrastructure.EfCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BlogManagement.Infrastructure.EfCore
{
    public class BlogContext : DbContext
    {
        public BlogContext(DbContextOptions<BlogContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            var assembly = typeof(ArticleMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            modelBuilder.Entity<Article>().HasQueryFilter(u => !u.IsDelete && u.ArticleCategory != null);
            modelBuilder.Entity<ArticleCategory>().HasQueryFilter(u => !u.IsDelete);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleCategory> ArticleCategories { get; set; }
    }
}