using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WhatappServer.Models;
using WhatsappDB;

namespace WhatappServer.Controllers
{
    public class MessageController : ApiController
    {
        [HttpGet]
        public List<Message> GetMessages()
        {
            var messageModel = new MessageModel();
            return messageModel.GetAllMessages();
        }

        [HttpGet]
        public void AddNewMessage(string sender, string receiver, DateTime date, string content)
        {
            var messageModel = new MessageModel();
            messageModel.AddNewMessage(sender, receiver, date, content);
        }

        [HttpGet]
        public void RemoveMessages( string sender,string receiver)
        {
            var messageModel = new MessageModel();
            messageModel.RemoveMessagesBetweenTwoUsers(sender, receiver);
        }

        [HttpGet]
        public List<Message> GetMessages( string firstUser, string secondUser)
        {
            var messageModel = new MessageModel();
            return messageModel.GetAllMessagesBetweenUsers(firstUser, secondUser);
        }

        [HttpGet]
        public Message GetLatestMessage(string clientUser, string friendUser)
        {
            var messageModel = new MessageModel();
            return messageModel.GetLastMessageFromFriend(clientUser, friendUser);
        }
    }
}
