using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly FundooContext fundooContext;
        private readonly IConfiguration _Appsettings;
        //Constructor
        public UserRL(FundooContext fundooContext, IConfiguration _Appsettings)
        {
            this.fundooContext = fundooContext;
            this._Appsettings = _Appsettings;
        }
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
        public string Login(string Email, string Password)
        {
            try
            {
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                    return null;
                //LINQ Query to match the input in database
                var result = this.fundooContext.UserTable.Where(x => x.Email == Email && x.Password == Password).FirstOrDefault();
                var id = result.Id;
                if (result != null)
                    //Calling Jwt Token method 
                    return GenerateSecurityToken(result.Email, id);
                else
                    return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //Method for Jwt Token For Login authentication with email and id
        private string GenerateSecurityToken(string Email, long Id)
        {
            //structures of JWT Token
            //header
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //payload
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            //signature
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
