using AutoMapper;
using BookLibrary.BLL.Interfaces;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Models.CustomModels.OrderModel;
using BookLibrary.DAL.Entities;
using BookLibrary.DAL.Entities.OrderModel;
using BookLibrary.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;
        private readonly ShoppingCart _shoppingCart;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, ShoppingCart shoppingCart)
        {
            Database = unitOfWork;
            _mapper = mapper;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrder(OrderData orderData)
        {
            orderData.OrderPlaced = DateTime.UtcNow.Date;
            
            var order = _mapper.Map<Order>(orderData);

            await Database.Orders.Create(order);

            var orderDetail = new List<OrderDetail>(_shoppingCart.Items.Count());

            foreach (var item in _shoppingCart.Items)
            {
                item.Book.InStock = Math.Max(item.Book.InStock - item.Quantity, 0);
                var book = _mapper.Map<Book>(item.Book);

                orderDetail.Add(
                new OrderDetail
                {
                    OrderId = order.Id,
                    BookId = item.Book.BookId,
                    Quantity = Math.Min(item.Quantity, item.Book.InStock),
                    Price = item.Book.Price,
                    Book = book,
                    Order = order
                });

                Database.Books.Edit(book);
                Database.Orders.EditOrderDetail(orderDetail);
                await Database.Save();
            }

            await Database.Save();
            
        }

        public async Task<OrderData> GetById(int orderId)
        {
            var order = await Database.Orders.GetById(orderId);
            var orderData = _mapper.Map<OrderData>(order);
         
            return orderData;
        }

        public async Task<IEnumerable<OrderData>> GetByUserId(string userId)
        {
            var orders = await GetAll();
            return orders.Where(order => order.UserId == userId && order.IsReturned == false);
        }

        public async Task<IEnumerable<OrderData>> GetAll()
        {  
            return _mapper.Map<IEnumerable<Order>, List<OrderData>>(await Database.Orders.GetAll());
        }

        public async Task CloseOrder(OrderData orderData)
        {
            var order = _mapper.Map<Order>(orderData);
            order.IsReturned = true;
            await Database.Orders.Close(order);
            await Database.Save();
        }
    }
}
