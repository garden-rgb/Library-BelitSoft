using BookLibrary.DAL.DatabaseSecret;
using BookLibrary.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;

        private BookRepository bookRepository;
        private UserRepository userRepository;
        private OrderRepository orderRepository;

        public UnitOfWork(StoreContext context)
        {
            _context = context;
        }

        public IBookRepository Books
        {
            get
            {
                if (bookRepository == null)
                    bookRepository = new BookRepository(_context);
                return bookRepository;
            }
        }

        public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(_context);
                return userRepository;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if (orderRepository == null)
                    orderRepository = new OrderRepository(_context);
                return orderRepository;
            }
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
