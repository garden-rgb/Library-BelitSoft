using System;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IBookRepository Books { get; }
        IUserRepository Users { get; }
        IOrderRepository Orders { get; }
        Task Save();
    }
}
