using BookLibrary.BLL.Models.CustomModels.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrder(OrderData order);
        Task<OrderData> GetById(int orderId);
        Task<IEnumerable<OrderData>> GetAll();
        Task<IEnumerable<OrderData>> GetByUserId(string userId);
        Task CloseOrder(OrderData orderData);
    }
}
