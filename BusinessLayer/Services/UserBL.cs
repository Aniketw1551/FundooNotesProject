using System;
using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;

        // Constructor of UserBL
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        // Method to return UserRegistration object 
        public User Registration(UserRegistration userRegistration)
        {
            try
            {
                return this.userRL.Registration(userRegistration);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Method to return login object
        public string Login(string email, string password)
        {
            try
            {
                return this.userRL.Login(email, password);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Method to return Forgot password object
        public string ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }

        // Method to return return password object
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                return this.userRL.ResetPassword(email, newPassword, confirmPassword);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
