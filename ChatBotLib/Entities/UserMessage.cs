﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChatBotLib.Entities
{
    public class UserMessage
    {
        /// <summary>
        /// Text of the message.
        /// </summary>
        public string MessageText { get; set; }

        /// <summary>
        /// Message after text preprocessing and tokenizing.
        /// </summary>
        public string[] MessageTextAsTokensArr { get; set; }

        /// <summary>
        /// Id of user who sended the message to the bot.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The name of the user who sended this message to bot.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// This is a project for which this message was sent from user.
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Raw user's message preprocessing.
        /// </summary>
        /// <param name="pp"></param>
        public void PreProcessOfrawMessage(Func<string, string[]> pp)
        {
            this.MessageTextAsTokensArr = pp(this.MessageText);
        }
    }
}
