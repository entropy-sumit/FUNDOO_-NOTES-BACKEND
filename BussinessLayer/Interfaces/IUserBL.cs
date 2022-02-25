using CommonLayer.Models;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interfaces
{
    public interface IUserBL
    {
        public User Registration(UserRegModel userRegModel); // making method of return type user

        public LoginResponseModel UserLogin(UserLoginmodel info);
        public string ForgetPassword(string email);
        public bool ResetPassword(string email, string password, string confirmPassword);

    }
}
