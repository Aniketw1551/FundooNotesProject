//-----------------------------------------------------------------------
// <copyright file="UserRL.cs" company="Aniket">
// Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace RepositoryLayer.Services
{
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

    /// <summary>
    /// User Class
    /// </summary>
    public class UserRL : IUserRL
    {
        ////Instance variables

        /// <summary> The FUNDOO CONTEXT</summary>
        private readonly FundooContext fundooContext;

        /// <summary>
        /// The APPSETTINGS
        /// </summary>
        private readonly IConfiguration appsettings;
        ////Constructor

        /// <summary>Initializes a new instance of the <see cref="UserRL" /> class.</summary>
        /// <param name="fundooContext">The FUNDOO CONTEXT.</param>
        /// <param name="appsettings">The APPSETTINGS.</param>
        public UserRL(FundooContext fundooContext, IConfiguration appsettings)
        {
            this.fundooContext = fundooContext;
            this.appsettings = appsettings;
        }
        ////Constructor of UserRL

        /// <summary>Initializes a new instance of the <see cref="UserRL" /> class.</summary>
        /// <param name="fundooContext">The FUNDOO CONTEXT.</param>
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
        }

        /// <summary>
        /// /Method for user registration
        /// </summary>
        /// <param name="userReg">The User Registration</param>
        /// <returns>User Details</returns>
        public User Registration(UserRegistration userReg)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userReg.FirstName;
                newUser.LastName = userReg.LastName;
                newUser.Email = userReg.Email;
                newUser.Password = Cipher.Encrypt(userReg.Password);
                this.fundooContext.UserTable.Add(newUser);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                    return newUser;
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
        /// /Method for login
        /// </summary>
        /// <param name="email">The Email</param>
        /// <param name="password">The Password</param>
        /// <returns>User Login</returns>
        public string Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                    return null;
                ////LINQ Query to match the input in database
                var result = this.fundooContext.UserTable.FirstOrDefault(x => x.Email == email);
                string decrypt = Cipher.Decrypt(result.Password);
                var id = result.Id;
                if (result != null && decrypt == password)
                    ////Calling Jwt Token method 
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
        /// Method for Forgot password token generation
        /// </summary>
        /// <param name="email">The Email</param>
        /// <returns>Password ChangeLink</returns>
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
        /// <param name="email">The Email</param>
        /// <param name="newPassword">The NewPassword</param>
        /// <param name="confirmPassword">The ConfirmPassword</param>
        /// <returns>Reset Password</returns>
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

        /// <summary>
        /// Method for JWT Token For Login authentication with email and id
        /// </summary>
        /// <param name="email">The Email</param>
        /// <param name="Id">The Id</param>
        /// <returns>Token</returns>
        private string GenerateSecurityToken(string email, long Id)
        {
            ////structures of JWT Token
            ////Header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appsettings["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            ////Payload
            var claims = new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim("Id", Id.ToString())
            };
            ////Signature
            var token = new JwtSecurityToken(appsettings["Jwt:Issuer"],
              appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
