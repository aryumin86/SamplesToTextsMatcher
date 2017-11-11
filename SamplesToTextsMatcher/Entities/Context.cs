using System;
using System.Collections.Generic;

namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Input string as tokens array where index is token's position
    /// (Context of interpret operations for terminal and non terminal expressions).
    /// </summary>
    public class Context
    {
        public char[] Input { get; private set; }

        /// <summary>
        /// Queue of expressions got from input.
        /// </summary>
        /// <value>The expressions queue.</value>
        public Queue<Expression> ExpressionsQueue { get; set; }

        /// <summary>
        /// Root of expressions binary tree.
        /// </summary>
        /// <value>The root.</value>
        public Expression Root { get; set; }

        public Context(string input){
            this.Input = input.ToCharArray();
        }
    }
}
