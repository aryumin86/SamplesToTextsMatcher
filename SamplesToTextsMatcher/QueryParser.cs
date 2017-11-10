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

        /// <summary>
        /// Removes double spaces, make to lower case the query string etc.
        /// </summary>
        /// <returns>The text first format.</returns>
        /// <param name="query">Query.</param>
        private string queryTextFirstFormat(string query){
            throw new NotImplementedException();
        }

        /// <summary>
        /// If there are asterix operators in query - resolves it - 
        /// appeal to morphological dictionary, gets the forms of words (if the
        /// needed words are there and returns arrays of words with maximum number of
        /// synonyms from argument of this method. 
        /// </summary>
        /// <returns>The query asterix operators.</returns>
        /// <param name="query">Raw query</param>
        /// <param name="tokenFormsMaxNumber">Maximum number of forms of token to 
        /// retrive from morphological dictionary</param>
        private string resolveQueryAsterixOperators(string query, int tokenFormsMaxNumber){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replace the word with its forms like  word1 -> (word1_1 OR word1_2 OR word1_3) 
        /// (unless it is not in form like =word1 or like "word1 word2"). 
        /// </summary>
        /// <returns>The words forms for token.</returns>
        /// <param name="query">Query.</param>
        private string getWordsFormsForToken(string query){
            throw new NotImplementedException();
        }
    }
}
