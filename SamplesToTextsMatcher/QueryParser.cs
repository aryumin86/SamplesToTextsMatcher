using System;
using SamplesToTextsMatcher.Entities;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Parses the input query, validates it structure and creates 
    /// binary tree of query. 
    /// </summary>
    public class QueryParser
    {
        /// <summary>
        /// Validate if a query string is logically valid.
        /// </summary>
        /// <returns><c>true</c>, if input was validated, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
        public bool ValidateInput(string input){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Parse input query and create tree.
        /// </summary>
        /// <returns>Tree's root.</returns>
        /// <param name="input">Input.</param>
        public Node ParseInput(string input){
            throw new NotImplementedException();
        }
    }
}
