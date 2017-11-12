using System;
using System.Collections.Generic;
using SamplesToTextsMatcher.Entities;
using System.Text.RegularExpressions;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Input string as tokens array where index is token's position
    /// (Context of interpret operations for terminal and non terminal expressions).
    /// </summary>
    public class Context
    {
        private string _input;

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
            this._input = input;
        }

        /// <summary>
        /// Removes double spaces, make to lower case the query string etc.
        /// </summary>
        /// <returns>The text first format.</returns>
        /// <param name="query">Query.</param>
        private void queryTextFirstFormat()
        {
            this._input = this._input.ToLower().Trim('\n','\r', ' ');
            this._input = Regex.Replace(this._input, @"\s+", " ");
        }

        /// <summary>
        /// Validate if a query string is logically valid.
        /// </summary>
        /// <param name="context">context.</param>
        public void ValidateInput()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parses input and creates binary tree.
        /// </summary>
        private void ParseInput(){
            
        }


    }
}
