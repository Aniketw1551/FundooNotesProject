using System;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IUserRL
    {
        public User Registration(UserRegistration userRegistration);

        public string Login(string email, string password);

        public string ForgotPassword(string email);

       public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
