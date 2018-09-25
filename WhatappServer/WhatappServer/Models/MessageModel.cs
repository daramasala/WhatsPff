using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhatappServer.Database;
using WhatappServer.Interfaces.Models;
using WhatsappDB;

namespace WhatappServer.Models
{
    public class MessageModel : IMessageModel
    {
        /// <summary>
        /// Adds a new message 
        /// </summary>
        /// <param name="senderUsername">Sender username</param>
        /// <param name="receiverUsername">Receiver username</param>
        /// <param name="date">Date of the message</param>
        /// <param name="content">Content of the message</param>
        public void AddNewMessage(string senderUsername, string receiverUsername, DateTime date, string content)
        {
            var db = DbUtility.context;
            try
            {
                db.Messages.Add(new Message(senderUsername, receiverUsername, date, content));
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Returns all the messages
        /// </summary>
        /// <returns></returns>
        public List<Message> GetAllMessages()
        {
            var db = DbUtility.context;
            return db.Messages.ToList();
        }

        /// <summary>
        /// Gets all the message where firstusername is the sender
        /// and the secondusername is the receiver
        /// </summary>
        /// <param name="firstUsername">First username</param>
        /// <param name="secondUsername">Second username</param>
        /// <returns></returns>
        public List<Message> GetAllMessagesBetweenUsers(string firstUsername, string secondUsername)
        {
            var db = DbUtility.context;
            try
            {
                var messages = from m in db.Messages
                               where (m.SenderUsername == firstUsername && m.RecieverUsername == secondUsername)
                               || (m.SenderUsername == secondUsername && m.RecieverUsername == firstUsername)
                               select m;
                return messages.ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        /// <summary>
        /// Gets the latest message between two users
        /// </summary>
        /// <param name="ownerUser">Client user</param>
        /// <param name="friendUser">Friend's username</param>
        /// <returns></returns>
        public Message GetLastMessageFromFriend(string ownerUser, string friendUser)
        {
            var db = DbUtility.context;
            try
            {
                var query = (from m in db.Messages
                             where ((m.SenderUsername == ownerUser && m.RecieverUsername == friendUser)
                             || (m.SenderUsername == friendUser && m.RecieverUsername == ownerUser))
                             select m).ToList();
                query = query.OrderByDescending(x => x.Date).ToList();
                if (query.Count == 0)
                    return null;
                return query[0];

            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Removes all the messages between given two usernames
        /// </summary>
        /// <param name="firstUsername">First username</param>
        /// <param name="secondUsername"Second username></param>
        public void RemoveMessagesBetweenTwoUsers(string firstUsername, string secondUsername)
        {
            var db = DbUtility.context;
            try
            {
                // Get all the message where firstusername is the sender and secondusername is the receiver
                List<Message> first = GetAllMessagesBetweenUsers(firstUsername, secondUsername);

                // Get all the message where secondusername is the sender and firstusername is the receiver
                List<Message> second = GetAllMessagesBetweenUsers(secondUsername, firstUsername);

                // Remove the messages
                foreach (Message m in first)
                {
                    db.Messages.Remove(m);
                }
                foreach (Message m in second)
                {
                    db.Messages.Remove(m);
                }

                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}