﻿using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
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
        IConfiguration _Appsettings;
        public UserRL(FundooContext fundooContext, IConfiguration Appsettings)
        {
            this.fundooContext = fundooContext;
            _Appsettings = Appsettings;
        }
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="userRegmodel"></param>
        /// <returns></returns>

        public User Registration(UserRegModel userRegModel)
        {
            try
            {
                User newUser = new User();
                newUser.FirstName = userRegModel.FirstName;
                newUser.LastName = userRegModel.LastName;
                newUser.Email = userRegModel.Email;
                newUser.Password = EncryptPassword(userRegModel.Password);

                fundooContext.UserTables.Add(newUser); // adding user to db
                int result = fundooContext.SaveChanges();
                if (result > 0)
                {
                    return newUser;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// All Registerd Login Data
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginmodel info)
        {
            try
            {
                var Enteredlogin = this.fundooContext.UserTables.Where(X => X.Email == info.Email).FirstOrDefault();
                if (DecryptPassword(Enteredlogin.Password) == info.Password)
                
                {
                    LoginResponseModel data = new LoginResponseModel();
                    string token = GenerateSecurityToken(Enteredlogin.Email, Enteredlogin.Id);
                    data.Id = Enteredlogin.Id;
                    data.FirstName = Enteredlogin.FirstName;
                    data.LastName = Enteredlogin.LastName;
                    data.Email = Enteredlogin.Email;
                    data.Password = Enteredlogin.Password;
                    data.Token = token;
                    return data;
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
        /// 
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        private string GenerateSecurityToken(string Email, long Id)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("Id",Id.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string ForgetPassword(string email)
        {
            try
            {
                var existingLogin = this.fundooContext.UserTables.Where(X => X.Email == email).FirstOrDefault();
                if (existingLogin != null)
                {
                    var token = GenerateSecurityToken(email, existingLogin.Id);
                    new MSMQmodel().MSMQSender(token);
                    return token;
                }
                else
                    return null;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    User user = fundooContext.UserTables.Where(e => e.Email == email).FirstOrDefault();
                    user.Password = confirmPassword;
                    fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public string EncryptPassword(string password)
        {
            string enteredpassword = "Hide";
            byte[] hide = new byte[password.Length];
            hide = Encoding.UTF8.GetBytes(password);
            enteredpassword = Convert.ToBase64String(hide);
            return enteredpassword;
        }
        public string DecryptPassword(string password)
        {
            string decryptpassword = string.Empty;
            UTF8Encoding encodepwd = new UTF8Encoding();
            Decoder Decode = encodepwd.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(password);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            decryptpassword= new String(decoded_char);
            return decryptpassword;
        }

    }
}
