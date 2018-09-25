using System;

namespace WhatsappClient
{
    /// <summary>
    /// Chat view items object
    /// </summary>
    public class ChatView
    {
        public string CurrentUserMessage { get; set; }
        public string FriendUserMessage { get; set; }
        public DateTime Time { get; set; }

        public ChatView()
        {

        }

        public ChatView(string currentUserMessage, string friendUserMessage, DateTime time)
        {
            CurrentUserMessage = currentUserMessage;
            FriendUserMessage = friendUserMessage;
            Time = time;
        }
    }
}
