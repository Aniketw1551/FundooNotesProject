using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        //Constructor of UserBL
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        //Method to return UserRegistration object 
        public User Registration(UserRegistration userRegistration)
        {
            try
            {
                return userRL.Registration(userRegistration);
            }
            catch (Exception)
            {

                throw;
            }
        }
        // Method to return login object
        public string Login(string Email, string Password)
        {
            try
            {
                return userRL.Login(Email, Password);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method to return Forgot password object
        public string ForgotPassword(string email)
        {
            try
            {
                return userRL.ForgotPassword(email);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
