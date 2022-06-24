using BookLibrary.DAL.DatabaseSecret;
using BookLibrary.DAL.Entities.OrderModel;
using BookLibrary.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Repositories
{
    class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;

        public OrderRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _context.Orders
                .Include(order => order.OrderLines)
                .ThenInclude(line => line.Book)
                .ToListAsync();
        }

        public async Task<Order> GetById(int id)
        {
            return await _context.Orders.FindAsync(id);
        }

        public async Task Create(Order order)
        {
            await _context.Orders.AddAsync(order);
        }

        public async Task Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            _context.Orders.Remove(order);
        }

        public void Update(Order order)
        {
            order.IsReturned = true;
            _context.SaveChanges();
        }

        public async Task Close(Order order)
        {
            _context.Orders.Update(order);
        }

        public void EditOrderDetail(List<OrderDetail> orderDetail)
        {
            _context.OrderDetails.AddRange(orderDetail);
        }
        
    }
}
