using BookLibrary.BLL.Models.CustomModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookData>> GetAll();
        Task<IEnumerable<BookData>> GetFilteredBooks(string search);
        Task<BookData> GetById(int id);
        Task NewBook(BookData book);
        Task EditBook(BookData book);
        Task DeleteBook(int id);
    }
}
