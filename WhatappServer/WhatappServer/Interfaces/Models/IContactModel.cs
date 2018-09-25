using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsappDB;

namespace WhatappServer.Models
{
    public interface IContactModel
    {
        /// <summary>
        /// Gets all the current contacts
        /// </summary>
        /// <returns>List of contacts</returns>
        List<Contact> GetContacts();

        /// <summary>
        /// Gets the requested contact
        /// </summary>
        /// <param name="requiredContact">Username</param>
        /// <returns></returns>
        Contact GetContact(string requiredContact);

        /// <summary>
        /// Checks if username already exists in the database
        /// </summary>
        /// <param name="username">username</param>
        /// <returns>ture if exists - false otherwise</returns>
         bool CheckIfContactExits(string username);

        /// <summary>
        /// Creates a new contact and add him to database
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="firstName">firstname</param>
        /// <param name="lastName">lastname</param>
        void AddNewContact(string username, string password, string firstName, string lastName);

        /// <summary>
        /// Login using given username and password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        int Login(string username, string password);

        /// <summary>
        /// Log out method with given username
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns></returns>
        int Logout(string username);

        /// <summary>
        /// Adds a new username to the friend list
        /// </summary>
        /// <param name="username">username</param>
        /// <returns></returns>
        int AddNewFriendToList(string currentUser,string username);

        /// <summary>
        /// Adds a new username to the friend list
        /// </summary>
        /// <param name="username">username</param>
        /// <returns></returns>
        //int RemoveFriendFromList(string currentUser, string username);

        /// <summary>
        /// Get all the friend list for a certin user
        /// </summary>
        /// <param name="username">User</param>
        /// <returns>List of friends</returns>
        List<Contact> GetFriendList(string username);
    }
}
