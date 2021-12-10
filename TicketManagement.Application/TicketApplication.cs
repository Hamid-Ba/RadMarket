using Framework.Application;
using Framework.Application.Authentication;
using Framework.Application.TicketComponents;
using System.Threading.Tasks;
using TicketManagement.Application.Contract.TicketAgg;
using TicketManagement.Domain.TicketAgg;

namespace TicketManagement.Application
{
    public class TicketApplication : ITicketApplication
    {
        private readonly IAuthHelper _authHelper;
        private readonly ITicketRepository _ticketRepository;

        public TicketApplication(IAuthHelper authHelper, ITicketRepository ticketRepository)
        {
            _authHelper = authHelper;
            _ticketRepository = ticketRepository;
        }

        public async Task<OperationResult> Create(CreateTicketVM command)
        {
            OperationResult result = new();

            try
            {
                if (command.UserId != _authHelper.GetStoreId()) return result.Failed("شما دسترسی به تیکت دیگران ندارید");

                if (string.IsNullOrWhiteSpace(command.Text)) return result.Failed("پیام نمی تواند خالی باشد");

                var ticket = new Ticket(command.UserId, command.Title, command.Section, TicketStatus.Received,
                    command.Necessary, true, false);

                await _ticketRepository.AddEntityAsync(ticket);
                await _ticketRepository.SaveChangesAsync();

                var message = new TicketMessage(ticket.Id, ticket.UserId, command.Text);
                ticket.AddMessage(message);

                await _ticketRepository.SaveChangesAsync();

                return result.Succeeded("پیام شما با موفقیت ارسال شد");
            }
            catch { return result.Failed(ApplicationMessage.GoesWrong); }
        }

        public async Task<OperationResult> AddMessage(AddMessageTicketVM command)
        {
            OperationResult result = new();

            try
            {
                if (command.UserId != _authHelper.GetStoreId()) return result.Failed("شما دسترسی به تیکت دیگران ندارید");
                if (_ticketRepository.Exists(t => t.UserId != command.UserId && t.Id == command.TicketId)) return result.Failed("شما دسترسی به تیکت دیگران ندارید");
                if (string.IsNullOrWhiteSpace(command.Text)) return result.Failed("پیام نمی تواند خالی باشد");

                var ticket = await _ticketRepository.GetEntityByIdAsync(command.TicketId);
                if (ticket is null) return result.Failed("همچین تیکتی وجود ندارد");

                ticket.WhoReadTicket(true, false);

                var message = new TicketMessage(command.TicketId, command.UserId, command.Text);
                ticket.AddMessage(message);

                await _ticketRepository.SaveChangesAsync();

                return result.Succeeded("پیام شما با موفقیت ارسال شد");
            }
            catch { return result.Failed(ApplicationMessage.GoesWrong); }

        }
    }
}
