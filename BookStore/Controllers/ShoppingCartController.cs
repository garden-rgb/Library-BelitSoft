using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Interfaces;
using BookLibrary.Web.Models.CustomModels.ViewModel;
using System.Net;

namespace BookLibrary.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ShoppingCart _shoppingCart;

        private const string InvalidAmountText = "There were not enough items in stock to add";

        public ShoppingCartController(IBookService bookService, ShoppingCart shoppingCart)
        {
            _bookService = bookService;
            _shoppingCart = shoppingCart;
        }

        [HttpGet]
        public IActionResult Index()
        {
            bool isValidAmount = true;

            var model = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.ComputeTotalValue(),
            };

            if (!isValidAmount)
            {
                ViewBag.InvalidAmountText = InvalidAmountText;
            }

            return View("Index", model);
        }


        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> Add(int id, int? amount = 1)
        {
            var book = await _bookService.GetById(id);

            var userId = User.FindFirst(ClaimTypes.Name).Value;

            if (book != null)
            {
                _shoppingCart.AddItem(book, userId, amount.Value);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int bookId)
        {
            var book = await _bookService.GetById(bookId);

            if (book != null)
            {
                _shoppingCart.RemoveLine(book);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ClearCart()
        {
             _shoppingCart.Clear();

            return RedirectToAction("Index");
        }
    }
}
