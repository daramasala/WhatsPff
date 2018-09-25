using WhatsappDB;

namespace WhatappServer.Database
{
    public static class DbUtility
    {
        // Initialize static WhatappDbContext
        public static WhatsappDBContext context = new WhatsappDBContext();
    }
}