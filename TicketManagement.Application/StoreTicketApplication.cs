using Framework.Application;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.StoreTicketAgg;
using TicketManagement.Domain.StoreTicketAgg;

namespace TicketManagement.Application
{
    public class StoreTicketApplication : IStoreTicketApplication
    {
        private readonly IStoreTicketRepository _storeTicketRepository;

        public StoreTicketApplication(IStoreTicketRepository storeTicketRepository) => _storeTicketRepository = storeTicketRepository;

        public async Task<OperationResult> Create(CreateStoreTicketVM command)
        {
            OperationResult result = new();

            if (command.FirstStoreId == command.SecondStoreId) return result.Failed("برای شرکت خودتون پیام میفرستید !!");
            if (string.IsNullOrWhiteSpace(command.Message)) return result.Failed("پیام نمی تواند خالی باشد");

            var ticket = new StoreTicket(command.Title, command.FirstStoreId, command.SecondStoreId);

            await _storeTicketRepository.AddEntityAsync(ticket);
            await _storeTicketRepository.SaveChangesAsync();

            var message = new StoreTicketMessage(ticket.Id, command.FirstStoreId, command.SecondStoreId, command.Message);
            ticket.AddMessage(message);

            await _storeTicketRepository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async Task<OperationResult> SendMessage(AddStoreTicketMessageVM command)
        {
            OperationResult result = new();

            var ticket = await _storeTicketRepository.GetEntityByIdAsync(command.StoreTicketId);

            if (ticket is null) return result.Failed(ApplicationMessage.NotExist);
            if (string.IsNullOrWhiteSpace(command.Message)) return result.Failed("پیام نمی تواند خالی باشد");

            command.ReciverId = command.SenderId == ticket.FirstStoreId ? ticket.SecondStoreId : ticket.FirstStoreId;

            var message = new StoreTicketMessage(command.StoreTicketId, command.SenderId, command.ReciverId, command.Message);
            ticket.AddMessage(message);

            await _storeTicketRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}