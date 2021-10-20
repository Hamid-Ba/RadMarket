using AccountManagement.Infrastructure.EfCore;
using DiscountManagement.Infrastructure.EfCore;
using Framework.Application;
using Microsoft.EntityFrameworkCore;
using RadMarket.Query.Contracts.OrderAgg;
using StoreManagement.Infrastructure.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadMarket.Query.Queries
{
    public class OrderQuery : IOrderQuery
    {
        private readonly StoreContext _storeContext;
        private readonly AccountContext _accountContext;
        private readonly DiscountContext _discountContext;

        public OrderQuery(StoreContext storeContext, AccountContext accountContext, DiscountContext discountContext)
        {
            _storeContext = storeContext;
            _accountContext = accountContext;
            _discountContext = discountContext;
        }

        public async Task<OrderQueryVM> GetBy(long userId)
        {
            var discounts = await _discountContext.Discounts.ToListAsync();
            int discountRate;
            List<ItemQueryVM> Items = new List<ItemQueryVM>();
            var order = await _storeContext.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(o => o.UserId == userId && !o.IsPayed);

            if (order is null) return null;

            foreach (var orderitem in order.OrderItems)
            {
                discountRate = 0;
                var item = new ItemQueryVM()
                {
                    Id = orderitem.Id,
                    OrderId = orderitem.OrderId,
                    ProductId = orderitem.ProductId,
                    UserId = order.UserId,
                    ProductTitle = orderitem.Product.Name,
                    ProductCode = orderitem.Product.Code,
                    Count = orderitem.Count
                };

                if (discounts.Any(c => c.ProductId == orderitem.ProductId && c.StartDate <= DateTime.Now && DateTime.Now <= c.EndDate))
                    discountRate = discounts.FirstOrDefault(c => c.ProductId == orderitem.ProductId && c.StartDate <= DateTime.Now && DateTime.Now <= c.EndDate).DiscountRate;

                if (discountRate > 0)
                {
                    item.DiscountRate = discountRate;
                    item.DiscountPrice = (orderitem.Product.PurchacePrice * discountRate) / 100;
                }

                item.PayAmount = orderitem.Product.PurchacePrice - item.DiscountPrice;
                item.UnitPrice = orderitem.Product.PurchacePrice;
                Items.Add(item);
            }

            var result = new OrderQueryVM
            {
                Id = order.Id,
                Items = Items
            };

            result.TotalPrice = result.Items.Sum(s => s.UnitPrice * s.Count);
            result.DiscountPrice = result.Items.Sum(s => s.DiscountPrice * s.Count);
            result.PayAmount = result.TotalPrice - result.DiscountPrice;

            return result;
        }

        public async Task<List<OrderQueryVM>> GetUserOrders(long userId, string code)
        {
            var result = _storeContext.Orders.Where(o => o.UserId == userId && o.IsPayed).Include(i => i.OrderItems).Select(o => new OrderQueryVM
            {
                Id = o.Id,
                TotalPrice = o.TotalPrice,
                DiscountPrice = o.DiscountPrice,
                PayAmount = o.PayAmount,
                PaymentMethod = o.PaymentMethod,
                IssueTracking = o.IssueTracking,
                PlaceOrderDate = o.PlaceOrderDate.ToFarsi(),
                GeorgianPlaceOrderDate = o.PlaceOrderDate
            }).AsNoTracking().OrderByDescending(o => o.GeorgianPlaceOrderDate).AsQueryable();

            if (!string.IsNullOrWhiteSpace(code)) result = result.Where(r => r.IssueTracking.Contains(code));

            return await result.ToListAsync();
        }

        public async Task<List<StoreOrderQueryVM>> GetStoreOrders(long storeId, string code)
        {
            var users = await _accountContext.User.Select(u => new { Id = u.Id, UserName = $"{u.FirstName} {u.LastName}" ,Mobile = u.Mobile}).ToListAsync();
            var items = await _storeContext.OrderItems.Where(o => o.Product.StoreId == storeId && o.Order.IsPayed)
                .Select(i => new {Id = i.Id , Payamount = i.PayAmount, DiscountPrice = i.DiscountPrice }).ToListAsync();

            var result = await _storeContext.OrderItems.Include(o => o.Order).Include(p => p.Product).Where(p => p.Product.StoreId == storeId).Where(o => o.Order.IsPayed).Select(s => new StoreOrderQueryVM
            {
                Id = s.Id,
                OrderId = s.OrderId,
                UserId = s.Order.UserId,
                IssueTracking = s.Order.IssueTracking,
                Count = s.Count,
                PlaceOrderDate = s.Order.PlaceOrderDate.ToFarsi(),
                PaymentMethod = s.Order.PaymentMethod,
                Status = s.Status,
                GeorgianPlaceOrderDate = s.Order.PlaceOrderDate
            }).AsNoTracking().OrderByDescending(o => o.GeorgianPlaceOrderDate).ToListAsync();

            result.ForEach(r => r.PayAmount = items.Where(i => i.Id == r.Id).Sum(p => p.Payamount));
            result.ForEach(r => r.DiscountPrice = items.Where(i => i.Id == r.Id).Sum(p => p.DiscountPrice));
            result.ForEach(r => r.TotalPrice = items.Where(i => i.Id == r.Id).Sum(p => p.Payamount * r.Count));

            result.ForEach(r => r.UserName = users.FirstOrDefault(u => u.Id == r.UserId)?.UserName);
            result.ForEach(r => r.UserMobile = users.FirstOrDefault(u => u.Id == r.UserId)?.Mobile);
                
            if (!string.IsNullOrWhiteSpace(code)) result = result.Where(r => r.IssueTracking.Contains(code)).ToList();

            return  result;
        }

        public async Task<List<ItemQueryVM>> GetUserItems(long orderId, long userId)
        {
            var result = await _storeContext.OrderItems.Include(p => p.Product).Where(o => o.OrderId == orderId && o.Order.UserId == userId).Select(o => new ItemQueryVM
            {
                Id = o.Id,
                ProductTitle = o.Product.Name,
                ProductCode = o.Product.Code,
                Count = o.Count,
                PayAmount = o.PayAmount,
                DiscountPrice = o.DiscountPrice,
                Status = o.Status
            }).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<ItemQueryVM> GetUserItemsForStore(long itemId, long storeId)
        {
            var result = await _storeContext.OrderItems.Include(p => p.Product).Where(o => o.Id == itemId && o.Product.StoreId == storeId).Select(o => new ItemQueryVM
            {
                Id = o.Id,
                UserId = o.Order.UserId,
                OrderId = o.OrderId,
                ProductTitle = o.Product.Name,
                ProductCode = o.Product.Code,
                Count = o.Count,
                PayAmount = o.PayAmount,
                DiscountPrice = o.DiscountPrice,
                Status = o.Status
            }).AsNoTracking().FirstOrDefaultAsync();

            return result;
        }

        public async Task<IEnumerable<StoreOrderQueryVM>> GetUnPayedItems(long storeId)
        {
            var users = await _accountContext.User.Select(u => new { Id = u.Id, UserName = $"{u.FirstName} {u.LastName}", Mobile = u.Mobile }).ToListAsync();
            var items = await _storeContext.OrderItems.Where(o => o.Product.StoreId == storeId && o.Order.IsPayed)
                .Select(i => new { Id = i.Id, Payamount = i.PayAmount, DiscountPrice = i.DiscountPrice }).ToListAsync();

            var result = await _storeContext.OrderItems
                .Include(o => o.Order)
                .Include(p => p.Product)
                .Where(p => p.Product.StoreId == storeId)
                .Where(o => o.Order.IsPayed)
                .Where(o => !o.IsPayedWithSite)
                .Select(s => new StoreOrderQueryVM
            {
                Id = s.Id,
                OrderId = s.OrderId,
                UserId = s.Order.UserId,
                IssueTracking = s.Order.IssueTracking,
                Count = s.Count,
                PlaceOrderDate = s.Order.PlaceOrderDate.ToFarsi(),
                PaymentMethod = s.Order.PaymentMethod,
                Status = s.Status,
                GeorgianPlaceOrderDate = s.Order.PlaceOrderDate,
                SiteProfitPercentage = s.SiteProfitPercentage,
            }).AsNoTracking().OrderByDescending(o => o.GeorgianPlaceOrderDate).ToListAsync();

            result.ForEach(r => r.PayAmount = items.Where(i => i.Id == r.Id).Sum(p => p.Payamount));
            result.ForEach(r => r.SiteProfitAmount = (r.PayAmount * r.Count) * r.SiteProfitPercentage / 100);
            result.ForEach(r => r.DiscountPrice = items.Where(i => i.Id == r.Id).Sum(p => p.DiscountPrice));
            result.ForEach(r => r.TotalPrice = items.Where(i => i.Id == r.Id).Sum(p => p.Payamount * r.Count));

            result.ForEach(r => r.UserName = users.FirstOrDefault(u => u.Id == r.UserId)?.UserName);
            result.ForEach(r => r.UserMobile = users.FirstOrDefault(u => u.Id == r.UserId)?.Mobile);

            return result;
        }

        public async Task<double> GetTotalSiteProfit(long storeId)
        {
            var result = await _storeContext.OrderItems
                .Where(o => o.Product.StoreId == storeId)
                .Where(o => !o.IsPayedWithSite)
                .Select(i => i.SiteProfitAmount).SumAsync(s => s);
            return result;
        }
    }
}