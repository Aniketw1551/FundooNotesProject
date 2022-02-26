using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IUserBL
    {
        public User Registration(UserRegistration userRegistration);
        public string Login(string Email, string Password);
    }
}
