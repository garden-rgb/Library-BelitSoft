using BookLibrary.DAL.Entities.OrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAll();
        Task<Order> GetById(int id);
        Task Create(Order order);
        Task Delete(int id);
        Task Close(Order order);
        void Update(Order order);
        void EditOrderDetail(List<OrderDetail> orderDetail);
    }
}
