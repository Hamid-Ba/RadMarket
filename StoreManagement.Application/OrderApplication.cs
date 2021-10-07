using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Domain.OrderAgg;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;

        public OrderApplication(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<long> CreateOrder(long userId)
        {
            var order = new Order(userId);

            await _orderRepository.AddEntityAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OrderVM> GetLastOpenedOrder(long userId)
        {
            if (!_orderRepository.Exists(o => o.UserId == userId && !o.IsPayed))
                await CreateOrder(userId);

            var openOrder = await _orderRepository.GetLastOpenOrderBy(userId);
            return openOrder;
        }

        public async Task<long> PlaceOrder(CreateOrderVM command)
        {
            var order = new Order(command.UserId, command.TotalPrice,command.DiscountPrice, command.PayAmount, command.Address, command.MobileNumber, command.PaymentMethod);

            await _orderRepository.AddEntityAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}