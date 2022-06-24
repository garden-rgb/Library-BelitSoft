using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BookLibrary.DAL.Interfaces;
using BookLibrary.DAL.DatabaseSecret;
using BookLibrary.DAL.Entities;

namespace BookLibrary.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly StoreContext _context;

        public BookRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.AsNoTracking().ToListAsync();
        }

        public async Task<Book> GetById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task Create(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);

            _context.Books.Remove(book);
        }

        public void Edit(Book book)
        {
            _context.Update(book);
        }

        public async Task<IEnumerable<Book>> GetFiltered(string searchString)
        {
            var books = from m in _context.Books select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                return books = books.Where(s => s.Title!.Contains(searchString));
            }

            return await books.ToListAsync();
        }
    }
}
