using CommonLayer.Model;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IUserRL
    {
        public User Registration(UserRegistration userRegistration);
        public string Login(string Email, string Password);
        public string ForgotPassword(string email);
    }
}
