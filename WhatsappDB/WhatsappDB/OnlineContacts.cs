namespace WhatsappDB
{
    public class OnlineContacts
    {
        
        public int Id { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public OnlineContacts()
        {

        }
        /// <summary>
        /// OnlineContacts constructor with given username
        /// </summary>
        /// <param name="username">Username</param>
        public OnlineContacts(string username)
        {
            this.Username = username;
        }
    }
}
