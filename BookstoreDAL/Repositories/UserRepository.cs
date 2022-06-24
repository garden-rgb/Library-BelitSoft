using BookLibrary.DAL.DatabaseSecret;
using BookLibrary.DAL.Entities;
using BookLibrary.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;

        public UserRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int? id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> FindByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> FindByEmailPassword(string email, string password)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
        }

        public async Task<UserRole> FindRole()
        {
            return await _context.Roles.FirstOrDefaultAsync(u => u.Name == "reader");
        }


        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            _context.Users.Remove(user);
        }
    }
}
