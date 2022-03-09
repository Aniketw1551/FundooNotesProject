using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System.Collections.Generic;


namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        ////Instance variables
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _Appsettings;
        ////Constructor
        public UserRL(FundooContext fundooContext, IConfiguration _Appsettings)
        {
            this.fundooContext = fundooContext;
            this._Appsettings = _Appsettings;
        }
        ////Constructor of UserRL
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }
        /// <summary>
        /// /Method for user registration
        /// </summary>
        /// <param name="userReg">User</param>
        /// <returns>User</returns>
       
        public User Registration(UserRegistration userReg)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userReg.FirstName;
                newUser.LastName = userReg.LastName;
                newUser.Email = userReg.Email;
                newUser.Password = Cipher.Encrypt(userReg.Password);
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

        /// <summary>
        /// /Method for login
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Login</returns>
        
        public string Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;
                // LINQ Query to match the input in database
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                string decrypt = Cipher.Decrypt(result.Password);
                var id = result.Id;
                if (result != null && decrypt == password)
                    // Calling Jwt Token method 
                    return GenerateSecurityToken(result.Email, id);
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method for Jwt Token For Login authentication with email and id
        /// </summary>
        /// <param name="Email">Email</param>
        /// <param name="Id">Id</param>
        /// <returns>Token</returns>
        
        private string GenerateSecurityToken(string email, long Id)
        {
            // structures of JWT Token
            // header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // payload
            var claims = new[] {
                new Claim(ClaimTypes.Email,email),
                new Claim("Id",Id.ToString())
            };
            // signature
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Method for Forgot password token generation
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Link</returns>
        
        public string ForgotPassword(string email)
        {
            try
            {
                ////checking Email exists or not
                var existingEmail = this.fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                if (existingEmail != null)
                {
                    //// Generating Token 
                    var token = GenerateSecurityToken(existingEmail.Email, existingEmail.Id);
                    //// Passing Token to MsmqModel
                    new MsmqModel().Sender(token);
                    return token;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Method for Reset password using token
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="newPassword">NewPassword</param>
        /// <param name="confirmPassword">ConfirmPassword</param>
        /// <returns>Reset</returns>
        
        public bool ResetPassword(string email, string newPassword, string confirmPassword)
        {
            try
            {
                ////Checking if new password matches with confirm password
                if (newPassword == confirmPassword)
                {
                    //// matching in database
                    var userP = this.fundooContext.UserTable.Where(x => x.Email == email).FirstOrDefault();
                    userP.Password = confirmPassword;
                    //// changing the old password to new password
                    this.fundooContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
