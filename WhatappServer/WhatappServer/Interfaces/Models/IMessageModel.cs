using System;
using System.Collections.Generic;
using WhatsappDB;

namespace WhatappServer.Interfaces.Models
{
    public interface IMessageModel
    {
        /// <summary>
        /// Gets all the messages in the database
        /// </summary>
        /// <returns>List of all the messages</returns>
        List<Message> GetAllMessages();

        /// <summary>
        /// Adds a new message to the database
        /// </summary>
        /// <param name="senderUsername">Sender username</param>
        /// <param name="receiverUsername">Receiver username</param>
        /// <param name="date">Date of the message</param>
        /// <param name="content">Content of the message</param>
        void AddNewMessage(string senderUsername, string receiverUsername, DateTime date, string content);

        /// <summary>
        /// Gets all the message for a specific user
        /// </summary>
        /// <param name="firstUsername">First username</param>
        /// <param name="secondUsername">Second username</param>
        /// <returns></returns>
        List<Message> GetAllMessagesBetweenUsers(string firstUsername, string secondUsername);

        /// <summary>
        /// Removes all the messages between two users
        /// </summary>
        /// <param name="firstUsername">First username</param>
        /// <param name="secondUsername">Second username</param>
        void RemoveMessagesBetweenTwoUsers(string firstUsername, string secondUsername);

        /// <summary>
        /// Gets the last message between the client user and his friend
        /// </summary>
        /// <param name="ownerUser">Client user's username</param>
        /// <param name="friendUser">Friend's username</param>
        /// <returns></returns>
        Message GetLastMessageFromFriend(string ownerUser, string friendUser);
    }
}
