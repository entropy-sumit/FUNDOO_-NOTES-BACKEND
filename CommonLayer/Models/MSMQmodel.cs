﻿using Experimental.System.Messaging;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CommonLayer.Models
{
    public class MSMQmodel
    {
        MessageQueue messageQueue = new MessageQueue();
        public void MSMQSender(string token)
        {
            messageQueue.Path = @".\private$\Token";//for windows path

            if (!MessageQueue.Exists(messageQueue.Path))
            {

                MessageQueue.Create(messageQueue.Path);

            }
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            messageQueue.ReceiveCompleted += MessageQueue_ReceiveCompleted;
            messageQueue.Send(token);
            messageQueue.BeginReceive();
            messageQueue.Close();
        }

        private void MessageQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            var message = messageQueue.EndReceive(e.AsyncResult);
            string token = message.Body.ToString();
            string Subject = "Fundoo Notes Reset Link";
            string Body = token;
            string JWT = DecodeJWT(token);
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("sum.24.hours@gmail.com", "Adding@123"),
                EnableSsl = true,
            };
            smtpClient.Send("sum.24.hours@gmail.com", JWT, Subject, Body);
            messageQueue.BeginReceive();
        }
        public string DecodeJWT(string token)
        {
            try
            {
                var DecodeToken = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadJwtToken((DecodeToken));
                var result = jsonToken.Claims.FirstOrDefault().Value;
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}