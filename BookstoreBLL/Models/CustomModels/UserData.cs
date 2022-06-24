using System;
using System.Collections.Generic;

﻿namespace BookLibrary.BLL.Models.CustomModels
{
    public class UserData
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public UserRoleData Role { get; set; }
    }
}
