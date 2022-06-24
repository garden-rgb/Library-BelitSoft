using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.Web.Models.CustomModels.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookLibrary.Web.Components
{
    public class ShoppingCartSummary : ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;

        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke()
        {
            bool isCorrect = true;
            var userName = User.Identity.Name;

            foreach(var item in _shoppingCart.Items)
            {
                if(item.UserName != userName) 
                {
                    isCorrect = false;
                }
            }

            if (isCorrect)
            {
                var model = new ShoppingCartViewModel
                {
                    ShoppingCart = _shoppingCart,
                    ShoppingCartTotal = _shoppingCart.ComputeTotalValue()
                };

                return View(model);
            }
            else
            {
                _shoppingCart.Clear();

                var model = new ShoppingCartViewModel
                {
                    ShoppingCart = _shoppingCart,
                    ShoppingCartTotal = _shoppingCart.ComputeTotalValue()
                };

                return View(model);
            }
        }

    }
}
