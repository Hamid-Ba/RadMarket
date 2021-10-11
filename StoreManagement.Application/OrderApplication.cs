using Framework.Application;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Domain.OrderAgg;
using StoreManagement.Domain.ProductAgg;
using System.Linq;
using System.Threading.Tasks;

namespace StoreManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderApplication(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        public async Task<OperationResult> AddProductToOpenOrder(AddOrderItemsVM command)
        {
            OperationResult result = new();

            if (!_productRepository.Exists(p => p.Id == command.ProductId)) return result.Failed(ApplicationMessage.NotExist);

            var openOrderVM = await GetLastOpenedOrder(command.UserId);
            var openOrder = await _orderRepository.GetEntityByIdAsync(openOrderVM.Id);

            var similarProduct = openOrder.OrderItems.FirstOrDefault(p => p.ProductId == command.ProductId);

            if (similarProduct is null)
            {
                var newItem = new OrderItem(command.ProductId, command.Count);
                openOrder.AddItem(newItem);
            }
            else similarProduct.AddCount(command.Count);

            await _orderRepository.SaveChangesAsync();
            return result.Succeeded();
        }

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
            var order = new Order(command.UserId, command.TotalPrice, command.DiscountPrice, command.PayAmount, command.Address, command.MobileNumber, command.PaymentMethod);

            await _orderRepository.AddEntityAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }
    }
}