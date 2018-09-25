using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WhatappServer.Database;
using WhatappServer.Models;
using WhatsappDB;

namespace WhatappServer.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// Gets all the contacts in the database
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        [Route("api/users/all")]
        [HttpPost]
        public List<Contact> GetAllUsers([FromBody]string s)
        {
            try
            {
                var contactModel = new ContactModel();
                return contactModel.GetContacts();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Add a new contact
        /// </summary>
        /// <param name="c">new contact</param>
        /// <returns>http response</returns>
        [HttpGet]
        public string CreateContact(string username, string password, string firstname, string lastname)
        {
            try
            {
                var contactModel = new ContactModel();
                if (!contactModel.CheckIfContactExits(username))
                {
                    contactModel.AddNewContact(username, password, firstname, lastname);
                    return ("Registertion Completed Successfully.");
                }
                return "User Already Exists!";
            }
            catch (Exception e)
            {
                return e.Message;
            }        
        }

        /// <summary>
        /// Login to Whatsapp server
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [HttpGet]
        public string Login(string username, string password)
        {
            var contactModel = new ContactModel();
            return contactModel.Login(username, password).ToString();
        }

        /// <summary>
        /// Logout method with given username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        [HttpGet]
        public int Logout(string username)
        {
            var contactModel = new ContactModel();
            return contactModel.Logout(username);
        }

        [HttpGet]
        public List<Contact> GetFriendList(string friendOfUsername)
        {
            var contactModel = new ContactModel();
            return contactModel.GetFriendList(friendOfUsername);
        }
        /// <summary>
        /// Add new user to friend list method
        /// </summary>
        /// <param name="currentUsername">The logged in user</param>
        /// <param name="newFriendUsername">The new friend username</param>
        /// <returns></returns>
        [HttpGet]
        public int AddNewFriend(string currentUsername, string newFriendUsername)
        {
            var contactModel = new ContactModel();
            return contactModel.AddNewFriendToList(currentUsername, newFriendUsername);
        }

        /// <summary>
        /// Add new user to friend list method
        /// </summary>
        /// <param name="currentUsername">The logged in user</param>
        /// <param name="newFriendUsername">The new friend username</param>
        /// <returns></returns>
        //[HttpGet]
        //public int RemoveFriend( string currentUsername, string friendUsername)
        //{
        //    var contactModel = new ContactModel();
        //    return contactModel.RemoveFriendFromList(currentUsername, friendUsername);
        //}

        [HttpGet]
        public Contact GetContact(string requestedUsername)
        {
            var contactModel = new ContactModel();
            return contactModel.GetContact(requestedUsername);
        }
    }
}
