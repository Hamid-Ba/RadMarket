using DiscountManagement.Infrastructure.EfCore;
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
        private readonly DiscountContext _discountContext;

        public OrderQuery(StoreContext storeContext, DiscountContext discountContext)
        {
            _storeContext = storeContext;
            _discountContext = discountContext;
        }

        public async Task<OrderQueryVM> GetBy(long userId)
        {
            var discounts = await _discountContext.Discounts.ToListAsync();
            int discountRate;
            List<ItemQueryVM> Items = new List<ItemQueryVM>();
            var order = await _storeContext.Orders.Include(o => o.OrderItems).ThenInclude(p => p.Product).FirstOrDefaultAsync(o => o.UserId == userId && !o.IsPayed);

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
    }
}