using Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Domain.OrderAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Infrastructure.EfCore.Repository
{
    public class OrderItemRepository : Repository<OrderItem>, IOrderItemRepository
    {
        private readonly StoreContext _context;

        public OrderItemRepository(StoreContext context) : base(context) => _context = context;

        public async Task<IEnumerable<OrderItemsVM>> GetAllPayedItems() => await _context.OrderItems
            .Where(i => i.Order.IsPayed)
            .Where(i => i.IsPayedWithSite)
            .Include(o => o.Order)
            .Include(p => p.Product)
            .ThenInclude(s => s.Store)
            .Select(i => new OrderItemsVM
            {
                Id = i.Id,
                OrderId = i.OrderId,
                StoreId = i.Product.StoreId,
                StoreName = i.Product.Store.Name,
                PayAmount = i.PayAmount,
                DiscountPrice = i.DiscountPrice,
                TotalPayAmount = i.PayAmount * i.Count,
                Status = i.Status,
                ProfitPercentage = i.SiteProfitPercentage,
                ProfitAmount = i.SiteProfitAmount,
                IssueTrackingCode = i.Order.IssueTracking
            }).AsNoTracking().ToListAsync();

        public async Task<IEnumerable<OrderItemsVM>> GetAllUnPayedItems() => await _context.OrderItems
            .Where(i => i.Order.IsPayed)
            .Where(i => !i.IsPayedWithSite)
            .Include(o => o.Order)
            .Include(p => p.Product)
            .ThenInclude(s => s.Store)
            .Select(i => new OrderItemsVM
        {
            Id = i.Id,
            OrderId = i.OrderId,
            StoreId = i.Product.StoreId,
            StoreName = i.Product.Store.Name,
            PayAmount = i.PayAmount,
            DiscountPrice = i.DiscountPrice,
            TotalPayAmount = i.PayAmount * i.Count,
            Status = i.Status,
            ProfitPercentage = i.SiteProfitPercentage,
            ProfitAmount = i.SiteProfitAmount,
            IssueTrackingCode = i.Order.IssueTracking
        }).AsNoTracking().ToListAsync();
        

        public async Task<ChangeOrderStatusVM> GetDetailForChangeStatus(long itemId) => await _context.OrderItems.Include(o => o.Order).Select(o => new ChangeOrderStatusVM
        {
            ItemId = o.Id,
            Status = o.Status,
            IssueTracking = o.Order.IssueTracking,
            UserId = o.Order.UserId,
            OrderId = o.OrderId
        }
        ).FirstOrDefaultAsync(o => o.ItemId == itemId);

        public async Task<IEnumerable<OrderItemsVM>> GetItems(long id) => await _context.OrderItems.Where(o => o.OrderId == id)
            .Include(p => p.Product)
            .ThenInclude(s => s.Store)
            .Select(p => new OrderItemsVM
            {
                Id = p.Id,
                OrderId = p.OrderId,
                ProductId = p.ProductId,
                ProductName = p.Product.Name,
                DiscountPrice = p.DiscountPrice,
                PayAmount = p.PayAmount,
                Count = p.Count,
                Status = p.Status,
                TotalPayAmount = (p.PayAmount * p.Count),
                StoreId = p.Product.StoreId,
                StoreName = p.Product.Store.Name,
                StoreCode = p.Product.Store.UniqueCode
            }).AsNoTracking().ToListAsync();
    }
}
