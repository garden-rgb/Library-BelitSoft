using AutoMapper;
using BookLibrary.BLL.Interfaces;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.DAL.Entities;
using BookLibrary.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Services
{
    public class BookService : IBookService
    {
        IUnitOfWork Database { get; set; }
        private readonly IMapper _mapper;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            Database = unitOfWork;
            _mapper = mapper;
        }

        public async Task DeleteBook(int id)
        {
            await Database.Books.Delete(id);

            await Database.Save();
        }

        public async Task EditBook(BookData bookData)
        {
            var book = _mapper.Map<Book>(bookData);

            Database.Books.Edit(book);
            await Database.Save(); 
        }

        public async Task<IEnumerable<BookData>> GetAll()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookData>()).CreateMapper();

            return mapper.Map<IEnumerable<Book>, List<BookData>>(await Database.Books.GetAll());
        }

        public async Task<BookData> GetById(int id)
        {
            var book = await Database.Books.GetById(id);

            var bookData = _mapper.Map<BookData>(book);

            return bookData;
        }

        public async Task NewBook(BookData bookData)
        {
            var book = _mapper.Map<Book>(bookData);

            await Database.Books.Create(book);
            await Database.Save();
        }

        public async Task <IEnumerable<BookData>> GetFilteredBooks(string searchQuery)
        {
            var queries = string.IsNullOrEmpty(searchQuery) ? null : Regex.Replace(searchQuery, @"\s+", " ").Trim().ToLower().Split(" ");
            var books = await GetAll();

            return books.Where(item => queries.Any(query => (item.Title.ToLower().Contains(query))));
        }

    }
}
