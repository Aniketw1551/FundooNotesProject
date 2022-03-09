//-----------------------------------------------------------------------
// <copyright file="IUserRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Interface
{
using System;
using CommonLayer.Model;
using RepositoryLayer.Entity;

    /// <summary>
    /// Interface class
    /// </summary>
    public interface IUserRL
    {
        /// <summary>Registrations the specified user registration.</summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public User Registration(UserRegistration userRegistration);

        /// <summary>Logins the specified email.</summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public string Login(string email, string password);

        /// <summary>Forgot the password.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public string ForgotPassword(string email);

        /// <summary>Resets the password.</summary>
        /// <param name="email">The email.</param>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool ResetPassword(string email, string newPassword, string confirmPassword);
    }
}
