//-----------------------------------------------------------------------
// <copyright file="UserController.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooNotes.Controllers
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]

    /// <summary>
    ///   User Controller API
    /// </summary>
    public class UserController : ControllerBase
    {
        /// <summary>The user BL</summary>
        private readonly IUserBL userBL;

        ////Constructor of USerController

        /// <summary>Initializes a new instance of the <see cref="UserController" /> class.</summary>
        /// <param name="userBL">The user BL.</param>
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        /// <summary>Registers the specified user registration.</summary>
        /// <param name="userRegistration">The user registration.</param>
        /// <returns>
        ///  User Registration API
        /// </returns>
        [HttpPost("Register")]
        public IActionResult Register(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Registration(userRegistration);
                if (result != null)
                    return this.Ok(new { Success = true, message = "User registration successful", data = result });
                else
                    return this.BadRequest(new { Success = false, message = "User registration Unsuccessful" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Logins the specified user login.</summary>
        /// <param name="userLogin">The user login.</param>
        /// <returns>
        /// User Login API
        /// </returns>
        [HttpPost("Login")]
        public IActionResult Login(UserLogin userLogin)
        {
            try
            {
                var user = userBL.Login(userLogin.Email, userLogin.Password);
                if (user != null)
                    return this.Ok(new { Success = true, message = "Suucessfully logged in", data = user });
                else
                    return this.BadRequest(new { Success = false, message = "Please enter valid email and password" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Forgot the password.</summary>
        /// <param name="email">The email.</param>
        /// <returns>
        /// Forgot Password API
        /// </returns>
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var token = userBL.ForgotPassword(email);
                if (token != null)
                    return this.Ok(new { Success = true, message = "Reset password email sent to your mail id ", data = token });
                else
                    return this.BadRequest(new { Success = false, message = "Error, Email not sent" });
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>Resets the password.</summary>
        /// <param name="newPassword">The new password.</param>
        /// <param name="confirmPassword">The confirm password.</param>
        /// <returns>
        ///  Reset Password API
        /// </returns>
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (userBL.ResetPassword(email, newPassword, confirmPassword))
                    return this.Ok(new { Success = true, message = "Password reset successfully" });
                else
                    return this.BadRequest(new { Success = false, message = "Error while resetting your password please try again" });
            }
            catch (Exception)
            { 
                throw;
            }
        }
    }
}