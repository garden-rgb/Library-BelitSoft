using System.Collections.Generic;

namespace BookLibrary.BLL.Models.CustomModels
{
    public class UserRoleData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<UserData> Users { get; set; }
        public UserRoleData()
        {
            Users = new List<UserData>();
        }
    }
}
