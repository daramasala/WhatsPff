using System;

namespace WhatsappDB
{
    public class Message
    {
        /// <summary>
        /// Message Properties
        /// </summary>
        public int MessageId { get; set; }
        public string SenderUsername { get; set; }
        public string RecieverUsername { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

        /// <summary>
        /// Each message belongs to one contact
        /// </summary>
        public virtual Contact ContactId { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Message()
        {
        }

        /// <summary>
        /// Message Constructor with parameters
        /// </summary>
        /// <param name="SUsername">Sender Username</param>
        /// <param name="RUsername">Reciever Username</param>
        /// <param name="date">Date of the message</param>
        /// <param name="content">Content of the message</param>
        public Message(string SUsername, string RUsername, DateTime date, string content)
        {
            SenderUsername = SUsername;
            RecieverUsername = RUsername;
            Date = date;
            Content = content;
        }
    }
}
