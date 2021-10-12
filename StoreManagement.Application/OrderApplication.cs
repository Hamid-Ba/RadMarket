using Framework.Application;
using StoreManagement.Application.Contract.OrderAgg;
using StoreManagement.Domain.OrderAgg;
using StoreManagement.Domain.ProductAgg;
using System.Linq;
using System.Threading.Tasks;
using Framework.Domain;

namespace StoreManagement.Application
{
    public class OrderApplication : IOrderApplication
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderApplication(IOrderRepository orderRepository, IProductRepository productRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderItemRepository = orderItemRepository;
        }

        public async Task<OperationResult> AddProductToOpenOrder(AddOrderItemsVM command)
        {
            OperationResult result = new();

            if (!_productRepository.Exists(p => p.Id == command.ProductId)) return result.Failed(ApplicationMessage.NotExist);

            var openOrderVM = await GetLastOpenedOrder(command.UserId);
            var openOrder = await _orderRepository.GetLastOpenOrderBy(command.UserId);

            var similarProduct = openOrder.OrderItems.FirstOrDefault(p => p.ProductId == command.ProductId);

            if (similarProduct is null)
            {
                var newItem = new OrderItem(openOrder.Id, command.ProductId, command.Count);
                await _orderItemRepository.AddEntityAsync(newItem);
            }
            else similarProduct.AddCount(command.Count);

            await _orderItemRepository.SaveChangesAsync();
            return result.Succeeded();
        }

        public async Task<long> CreateOrder(long userId)
        {
            var order = new Order(userId);

            await _orderRepository.AddEntityAsync(order);
            await _orderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async Task<OperationResult> DeleteItemBy(long userId, long itemId)
        {
            OperationResult result = new();

            var order = await _orderRepository.GetLastOpenOrderBy(userId);
            if (order is null) return result.Failed(ApplicationMessage.NotExist);

            var item = order.OrderItems.FirstOrDefault(o => o.Id == itemId);
            if (item is null) return result.Failed(ApplicationMessage.NotExist);

            item.Delete();
            await _orderItemRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<string> GetIssueTrackingBy(long id)
        {
            var order = await _orderRepository.GetEntityByIdAsync(id);
            return order.IssueTracking;
        }

        public async Task<OrderVM> GetLastOpenedOrder(long userId)
        {
            if (!_orderRepository.Exists(o => o.UserId == userId && !o.IsPayed))
                await CreateOrder(userId);

            var openOrder = await _orderRepository.GetLastOpenOrderVMBy(userId);
            return openOrder;
        }

        public async Task<OperationResult> PlaceOrder(CreateOrderVM command)
        {
            OperationResult result = new();

            var order = await _orderRepository.GetLastOpenOrderBy(command.UserId);
            if (order is null) return result.Failed(ApplicationMessage.NotExist);
            if (order.IsPayed) return result.Failed("این سفارش قبلا ثبت شده");

            var issue = order.PaymentSuccedded(command.PaymentMethod);
            order.SetOrderStatus(OrderStatus.OrderCreated);

            await _orderRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}