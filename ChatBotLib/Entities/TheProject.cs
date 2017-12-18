﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ChatBotLib.Entities
{
    /// <summary>
    /// Project is for 1 bot.
    /// </summary>
    public class TheProject
    {
        public int Id { get; set; }

        public string ProjectTitle { get; set; }

        public string ProjectDescription { get; set; }

        public int OwnerId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
