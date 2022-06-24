using BookLibrary.BLL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookLibrary.Web.Models.CustomModels
{
    public class SendMessage
    {
        private readonly IOrderService _orderService;
        private readonly IEmailService _emailService;

        private const string messageTopic = "Book library";

        public SendMessage(IEmailService emailService, IOrderService orderService)
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
                //await _emailService.SendEmailAsync(item.UserId, messageTopic, $"You need to return your book in {daysLeft} days");
                if (daysLeft <= 3 && daysLeft > 0)
                {
                    await _emailService.SendEmailAsync(item.UserId, messageTopic, $"You need to return your book in {daysLeft} days");
                    //RecurringJob.AddOrUpdate("sendmessagewarning", () => _emailService.SendEmailAsync(item.UserId, messageTopic, $"You need to return your book in {daysLeft} days"), Cron.Daily);
                }
                else if (daysLeft < 0)
                {
                    daysLeft = Math.Abs((item.ReturnDate.Date - DateTime.UtcNow.Date).TotalDays);

                    await _emailService.SendEmailAsync(item.UserId, messageTopic, $"You overdue your book by {daysLeft} days");
                }
            }
        }
    }
}
