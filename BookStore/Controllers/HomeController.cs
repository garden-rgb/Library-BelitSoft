using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookLibrary.Web.Models.CustomModels.ViewModel;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Interfaces;
using System;

namespace BookLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public HomeController(IBookService bookService, IMapper mapper)
        {
            _bookService = bookService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            IEnumerable<BookData> booksData = await _bookService.GetAll();
            if (!string.IsNullOrEmpty(searchString))
            {
                booksData = await _bookService.GetFilteredBooks(searchString);
            }

            var books = _mapper.Map<IEnumerable<BookData>, List<BookViewModel>>(booksData);

            return View(books);
        }

        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "librarian")]
        [HttpPost]
        public async Task<IActionResult> Create(BookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var bookData = _mapper.Map<BookData>(book);
                
                await _bookService.NewBook(bookData);

                return RedirectToAction(nameof(Index));
            }

            return View(book);
        }


        public async Task<IActionResult> Edit(int id)
        {
            BookData bookData = await _bookService.GetById(id);

            var book = _mapper.Map<BookViewModel>(bookData);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "librarian")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, BookViewModel book)
        {
            if (id != book.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var bookData = _mapper.Map<BookData>(book);

                    await _bookService.EditBook(bookData);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (id == 0)
                    {
                        return NotFound();
                    }

                    throw;
                }
                return RedirectToAction("Index");
            }

            return View(book);
        }

        [Authorize(Roles = "librarian")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            BookData bookData = await _bookService.GetById(id);

            var book = new BookViewModel { BookId = bookData.BookId };

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [Authorize(Roles = "librarian")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _bookService.DeleteBook(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
