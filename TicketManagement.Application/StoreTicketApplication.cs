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

            var message = new StoreTicketMessage(command.StoreTicketId, command.SenderId, command.ReciverId, command.Message);
            ticket.AddMessage(message);

            await _storeTicketRepository.SaveChangesAsync();

            return result.Succeeded();
        }
    }
}