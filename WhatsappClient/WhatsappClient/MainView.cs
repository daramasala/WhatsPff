using System;

namespace WhatsappClient
{
    /// <summary>
    /// Main view items object
    /// </summary>
    public class MainView
    {
        public string FullName { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Username { get; set; }

        public MainView() { }

        public MainView(string username, string message)
        {
            FullName = username;
            Message = message;
        }

        public MainView(string fullName, string message, DateTime date, string username)
        {
            Date = date;
            FullName = fullName;
            Message = message;
            Username = username;
        }
    }
}
