using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        //Constructor of USerController
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        //Registration of User
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
        // Login of User 
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
        //User Forgot Password Api
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
        //User Reset Password Api
        [HttpPost("ResetPassword")]
        public IActionResult ResetPassword(string newPassword, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                if (userBL.ResetPassword(email,newPassword,confirmPassword))
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
