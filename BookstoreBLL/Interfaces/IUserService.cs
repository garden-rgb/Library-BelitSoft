using BookLibrary.BLL.Models.CustomModels;
using BookLibrary.DAL.Entities;
using System.Threading.Tasks;

namespace BookLibrary.BLL.Interfaces
{
    public interface IUserService
    {
        Task<User> Register(UserData user);
        Task<User> Login(UserData user);
    }
}
