using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        //Constructor of UserRL
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        //Method for user registration
        public User Registration(UserRegistration userReg)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userReg.FirstName;
                newUser.LastName = userReg.LastName;
                newUser.Email = userReg.Email;
                newUser.Password = userReg.Password;
                fundooContext.UserTable.Add(newUser);
                int result = fundooContext.SaveChanges();
                if (result > 0)
                    return newUser;
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method for login
        public User Login(string Email, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                    return null;
                //LINQ Query to match the input in database
                var result = this.fundooContext.UserTable.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
                if (result == null)
                    return null;
                else
                    return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
