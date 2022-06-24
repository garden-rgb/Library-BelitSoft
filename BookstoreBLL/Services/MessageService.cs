using BookLibrary.BLL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;

        private const string messageTopic = "Book library";

        public MessageService(IEmailService emailService, IOrderService orderService)
        {
            _emailService = emailService;
            _orderService = orderService;
        }
        public async Task SendMessageWarning()
        {
            var orderData = await _orderService.GetAll();
            foreach (var item in orderData)
            {
                var daysLeft = (item.ReturnDate.Date - DateTime.UtcNow.Date).TotalDays;
                if (daysLeft <= 3 && daysLeft > 0 && item.IsReturned == false)
                {
                    await _emailService.SendEmailAsync(item.UserId, messageTopic, $"You need to return your book in {daysLeft} days");
                }
                else if (daysLeft < 0 && item.IsReturned == false)
                {
                    daysLeft = Math.Abs(daysLeft);

                    await _emailService.SendEmailAsync(item.UserId, messageTopic, $"You overdue your book by {daysLeft} days");
                }
            }
        }
    }
}
