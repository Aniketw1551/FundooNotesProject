using System;
using CommonLayer.Model;
using RepositoryLayer.Entity;

namespace BusinessLayer.Interface
{
   public interface IUserBL
    {
        public User Registration(UserRegistration userRegistration);

        public string Login(string email, string password);

        public string ForgotPassword(string email);

        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
