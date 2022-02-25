using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IUserRL
    {
        public User Registration(UserRegModel userRegModel);
        /// <summary>
        /// UserLogin for All login details
        /// </summary>
        /// <param name="user1"></param>
        /// <returns></returns>
        public LoginResponseModel UserLogin(UserLoginmodel info);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);
    }
}
