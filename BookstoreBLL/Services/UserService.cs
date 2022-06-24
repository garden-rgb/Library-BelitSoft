using BookLibrary.BLL.Interfaces;
using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.DAL.Entities;
using BookLibrary.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork Database;

        public UserService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public async Task<User> Register(UserData userDto)
        {
            User user = await Database.Users.FindByEmail(userDto.Email);

            if (user == null)
            {
                user = new User { Email = userDto.Email, Password = userDto.Password };
                UserRole role = await Database.Users.FindRole();

                if (role != null)
                {
                    user.Role = role;
                }

                await Database.Users.Create(user);
                await Database.Save();
            }

            return user;
        }

        public async Task<User> Login(UserData userDto)
        {
            User user = await Database.Users.FindByEmailPassword(userDto.Email, userDto.Password);
            return user;
        }
    }
}
