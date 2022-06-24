using BookLibrary.Web.Models.CustomModels.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookLibrary.Web.Components
{
    public class AccountSummary : ViewComponent
    {
        public AccountSummary()
        {
        }

        public IViewComponentResult Invoke()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var model = new OrderViewModel
                {
                    UserId = userName
                };

                return View(model);
            }

            return View();
        }
    }
}
