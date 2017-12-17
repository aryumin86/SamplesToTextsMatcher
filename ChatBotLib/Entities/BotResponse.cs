using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotLib.Entities
{
    /// <summary>
    /// Response of a bot to a user.
    /// </summary>
    public class BotResponse
    {
        public int Priority { get; set; }

        public string ResponseText { get; set; }

        public BotResponse(string responseText)
        {
            Priority = 1;
            this.ResponseText = responseText;
        }
    }
}
