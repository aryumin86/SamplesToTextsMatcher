using SamplesToTextsMatcher.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Word.
    /// </summary>
    public class TheWord
    {
        public int Id { get; set; }
        /// <summary>
        /// Id of this word in normal form.
        /// </summary>
        public int NormalFormId { get; set; }
        public string Raw { get; set; }
        /// <summary>
        /// Language of this word.
        /// </summary>
        public Lang Lang { get; set; }
        /// <summary>
        /// This is a normal form of word.
        /// </summary>
        public bool IsNormalForm { get; set; }
    }
}
