
﻿using AutoMapper;
using BookLibrary.BLL.Interfaces;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.BLL.Models.CustomModels.OrderModel;
using BookLibrary.Web.Models.CustomModels.ViewModel;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookLibrary.Web.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        private readonly ShoppingCart _shoppingCart;

        private const string emptyCartError = "Your cart is empty, add some items first";
        private const string CheckoutCompleteMessage = "Thanks for your order";
        private const string messageTopic = "Book library";

        public OrderController(IOrderService orderService, IBookService bookService, IUserService userService, 
        IMapper mapper, IEmailService emailService, ShoppingCart shoppingCart)
        {
            _mapper = mapper;
            _orderService = orderService;
            _shoppingCart = shoppingCart;
            _userService = userService;
            _bookService = bookService;
            _emailService = emailService;
        }

        [HttpGet]
        public async Task<IActionResult> UserOrders()
        {
            var userId = User.FindFirst(ClaimTypes.Name).Value;
            var model = await _orderService.GetByUserId(userId);

            var orders = _mapper.Map<IEnumerable<OrderData>, IEnumerable<OrderViewModel>>(model);
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var model = await _orderService.GetAll();
            var orders = _mapper.Map<IEnumerable<OrderData>, IEnumerable<OrderViewModel>>(model);
            
            return View(orders);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            var items = _shoppingCart.Items;

            if (!items.Any())
            {
                ModelState.AddModelError("", emptyCartError);
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public IActionResult Checkout(OrderViewModel model)
        {
            var items = _shoppingCart.Items;

            if (!items.Any())
            {
                ModelState.AddModelError("", emptyCartError);
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                model.OrderTotal = items.Sum(item => item.Quantity * item.Book.Price);

                model.IsReturned = false;
                model.UserId = User.FindFirst(ClaimTypes.Name).Value;

                var orderData = _mapper.Map<OrderData>(model);
                _orderService.CreateOrder(orderData);
                _shoppingCart.Clear();

                return RedirectToAction("CheckoutComplete");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CloseOrder(int id)
        {
            var orderData = await _orderService.GetById(id);

            var order = _mapper.Map<OrderViewModel>(orderData);

            if (orderData == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> CloseOrder(OrderViewModel orderViewModel)
        {
            var orderData = _mapper.Map<OrderData>(orderViewModel);

            await _orderService.CloseOrder(orderData);

            return RedirectToAction("AllOrders");
        }

        [HttpGet]
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = CheckoutCompleteMessage;
            return View();
        }

    }
}
