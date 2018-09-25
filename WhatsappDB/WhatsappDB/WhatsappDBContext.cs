using System.Data.Entity;

namespace WhatsappDB
{
    public class WhatsappDBContext : DbContext
    {
        /// <summary>
        /// DbSets of Contacts and Messages
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<OnlineContacts> OnlineContacts { get; set; }
        public DbSet<FriendList> FriendList { get; set; }

        /// <summary>
        /// Default Constructor
        /// 
        /// <c>Runs the DbContext Constructor with a given database name</c>
        /// </summary>
        public WhatsappDBContext()
            : base("WhatsappDBNew")
        {
            
        }
    }

    
}
