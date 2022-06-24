using System.Collections.Generic;

namespace BookLibrary.DAL.Entities

{
    public class UserRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public UserRole()
        {
            Users = new List<User>();
        }
    }
}
