using BookLibrary.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<IEnumerable<Book>> GetFiltered(string searchString);
        Task<Book> GetById(int id);
        Task Create(Book book);
        Task Delete(int id);
        void Edit(Book book);
    }
}
