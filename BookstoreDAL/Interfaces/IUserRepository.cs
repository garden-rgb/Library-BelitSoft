using BookLibrary.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> FindByEmail(string email);
        Task<User> FindByEmailPassword(string email, string password);
        Task<UserRole> FindRole();
        Task Create(User user);
        Task Delete(int id);
        Task<User> GetById(int? id);
    }
}
