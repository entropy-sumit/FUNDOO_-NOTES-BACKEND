using BussinessLayer.Interfaces;
using CommonLayer.Models;
using RepositoryLayer.Entities;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Services
{
    public class UserBL : IUserBL
    {
        private readonly IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

       
        public User Registration(UserRegModel userRegModel) // implementing interface
        {
            try
            {
                return userRL.Registration(userRegModel);
            }
            catch (Exception)
            {

                throw;
            }
        }
        LoginResponseModel IUserBL.UserLogin(UserLoginmodel user)
        {
            try
            {
                return userRL.UserLogin(user);
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
