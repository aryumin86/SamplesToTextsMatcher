using ChatBotLib.Entities;
using SamplesToTextsMatcher;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace ChatBotLib
{
    public class ChatBotContext : DbContext
    {
        public ChatBotContext() : base("chatBotConn")
        {
             
        }

        public DbSet<TheProject> TheProjects { get; set; }
        public DbSet<BotResponse> BotResponses { get; set; }
        public DbSet<UserMessage> UserMessages { get; set; }
        public DbSet<Context> Contexts { get; set; }
    }
}
