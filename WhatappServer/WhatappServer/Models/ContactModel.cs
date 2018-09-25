using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhatappServer.Database;
using WhatsappDB;

namespace WhatappServer.Models
{
    public class ContactModel : IContactModel
    {
        /// <summary>
        /// Creates a new contact and add it to the database
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <param name="firstName">firstname</param>
        /// <param name="lastName">lastname</param>
        public void AddNewContact(string username, string password, string firstName, string lastName)
        {
            try
            {
                var db = DbUtility.context;
                db.Contacts.Add(new Contact(username, password, firstName, lastName));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }   
        }

        /// <summary>
        /// Add a new username to contact list
        /// </summary>
        /// <param name="currentUser">The logged in user</param>
        /// <param name="username">The new contact</param>
        /// <returns></returns>
        public int AddNewFriendToList(string currentUser, string username)
        {
            var db = DbUtility.context;
            
            try
            {
                // Check if the username exists first
                if (!CheckIfContactExits(username))
                    return -1;

                // Check if friend already exists
                var res = db.FriendList.FirstOrDefault(x => x.Username == currentUser && x.Friend == username);
                if (res != null)
                    return -2;
                //Add both to friend list
                db.FriendList.Add(new FriendList(currentUser, username));
                db.FriendList.Add(new FriendList(username, currentUser));

                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -3;
            }
        }

        /// <summary>
        /// Checks if given username alerady exists in the database
        /// </summary>
        /// <param name="username">username</param>
        /// <returns> true = exists, false otherwise</returns>
        public bool CheckIfContactExits(string username)
        {
            try
            {
                var db = DbUtility.context;
                var query = from c in db.Contacts
                                   where username == c.Username
                                   select c;
                return query.ToList().Count != 0;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        /// <summary>
        /// Gets the requested contact
        /// </summary>
        /// <param name="requiredContact">Username</param>
        /// <returns></returns>
        public Contact GetContact(string requiredContact)
        {
            var db = DbUtility.context;
            return db.Contacts.FirstOrDefault(x => x.Username == requiredContact);
        }

        /// <summary>
        /// Gets all the contacts
        /// </summary>
        /// <returns>List of contacts</returns>
        public List<Contact> GetContacts()
        {
            var db = DbUtility.context;
            return db.Contacts.ToList();
        }


        /// <summary>
        /// Gets all the friend list
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>List of friends</returns>
        public List<Contact> GetFriendList(string username)
        {
            var db = DbUtility.context;
            try
            {
                List<Contact> friendList = new List<Contact>();
                var users = db.FriendList.Where(x => x.Username == username);
                if (users == null)
                    return null;
                foreach(FriendList friend in users)
                {
                    friendList.Add(GetContact(friend.Friend));
                }               
                return friendList;
            }
            catch (Exception e)
            {

                throw e;
            }
            
        }

        /// <summary>
        /// Login and add the user to OnlineContacts table
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        public int Login(string username, string password)
        {
            // If username does not exist
            if (!CheckIfContactExits(username))
                return -1;
            try
            {
                var db = DbUtility.context;
                var query = from c in db.Contacts
                            where c.Username == username && c.Password == password
                            select c;
                if (query.ToList().Count == 0)
                    return -2; // Username or password are incorrect

                // Check if user already logged in
                OnlineContacts check = db.OnlineContacts.FirstOrDefault(x => x.Username == username);
                if (check != null)
                    return -3;
                // username and password are correct, add to online user table
                db.OnlineContacts.Add(new OnlineContacts(username));
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -4;
            }
        }

        /// <summary>
        /// Perform logout for a given username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public int Logout(string username)
        {
            var db = DbUtility.context;
            try
            {
                var online = db.OnlineContacts.Where(x => x.Username == username);

                if (online.ToList().Count == 0)
                    return -1;

                foreach (OnlineContacts o in db.OnlineContacts)
                {
                    if (o.Username == username)
                        db.OnlineContacts.Remove(o);
                }
                db.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                return -2;
            }
        }

        /// <summary>
        /// Remove username from contact list
        /// </summary>
        /// <param name="currentUser">The logged in user</param>
        /// <param name="username">The request user</param>
        /// <returns></returns>
        //public int RemoveFriendFromList(string currentUser, string username)
        //{
        //    return -1;
        //    //var db = DbUtility.context;
        //    //try
        //    //{
        //    //    // Check if the username exists first
        //    //    if (!CheckIfContactExits(username))
        //    //        return -1;
        //    //    Contact currUser = db.Contacts.First(x => x.Username == currentUser);
        //    //    Contact newFriend = db.Contacts.First(x => x.Username == username);
        //    //    currUser.FriendList.Remove(newFriend);
        //    //    newFriend.FriendList.Remove(currUser);
        //    //    db.SaveChanges();
        //    //    return 1;

        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return -2;
        //    //}
        //}
    }
}