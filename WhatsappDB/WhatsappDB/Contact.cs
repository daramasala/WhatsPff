using System.Collections.Generic;

namespace WhatsappDB
{
    public class Contact
    {
        /// <summary>
        /// Contact Properites
        /// </summary>
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set;}
        public string LastName { get; set; }

        /// <summary>
        /// Relationship variables
        /// </summary>
        public virtual ICollection<Message> Messages { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Contact()
        {
        }

        /// <summary>
        /// Contact Constructor with given parameters
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="fname">First Name</param>
        /// <param name="lname">Last Name</param>
        
        public Contact(string username, string password, string fname, string lname)
        {
            Username = username;
            Password = password;
            FirstName = fname;
            LastName = lname;
            Messages = new HashSet<Message>();
        }
    }
}

