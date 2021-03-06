using System.Collections.Generic;

namespace BookLibrary.Web.Models.CustomModels
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
