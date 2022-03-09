//-----------------------------------------------------------------------
// <copyright file="UserBL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace BusinessLayer.Services
{
using System;
using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;

    /// <summary>
    ///  User Class
    /// </summary>
    public class UserBL : IUserBL
    {
        /// <summary>The user RL</summary>
        private readonly IUserRL userRL;

        ////Constructor of UserBL

        /// <summary>Initializes a new instance of the <see cref="UserBL" /> class.</summary>
        /// <param name="userRL">The user RL.</param>
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        ////Method to return UserRegistration object 

        /// <summary>Registrations the specified user registration.</summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>
        ///  User Registration
        /// </returns>
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

        /// <summary>Logins the specified email.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        /// User Login
        /// </returns>
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

        /// <summary>Forgot the password.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///  Forgot Password
        /// </returns>
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

        /// <summary>Resets the password.</summary>
        /// <param name="email">The email.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>
        /// Reset Password
        /// </returns>
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
