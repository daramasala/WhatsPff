namespace WhatsappDB
{
    public class FriendList
    {
        
        public int Id { get; set; }
        public string Username { get; set; }
        public string Friend { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public FriendList()
        {

        }
        /// <summary>
        /// FriendList constructor with given username and friend's username
        /// </summary>
        /// <param name="username">Username</param>
        public FriendList(string username, string friend)
        {
            Username = username;
            Friend = friend;
        }
    }
}
