﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStoresWebAPI.Models
{
    public class UserWithToken : User
    {
        
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }

        public UserWithToken(User user)
        {
            this.UserId = user.UserId;
            this.EmailAddress = user.EmailAddress;            
            this.FirstName = user.FirstName;
            
            this.LastName = user.LastName;
             
            this.HireDate = user.HireDate;

            this.Role = user.Role;
        }
    }
}
