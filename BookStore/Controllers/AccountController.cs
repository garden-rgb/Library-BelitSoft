using BookLibrary.BLL.Interfaces;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.Web.Models.CustomModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookLibrary.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private static readonly string cookie = "ApplicationCookie";
        private static readonly string errorMessage = "Неверный логин или пароль.";

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserData userDto = new UserData { Email = model.Email, Password = model.Password };

                var user = await _userService.Login(userDto);

                if (user == null)
                {
                    ModelState.AddModelError("", errorMessage);
                }
                else
                {
                    await Authenticate(user.Email, user.Role.Name);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserData userDto = new UserData
                {
                    Email = model.Email,
                    Password = model.Password,
                    RoleId = 2
                };

                var user = await _userService.Register(userDto);
                await Authenticate(user.Email, user.Role.Name);

                return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", errorMessage);

            return View(model);
        }

        private async Task Authenticate(string email, string roleName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, roleName)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, cookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
