using System;
using SamplesToTextsMatcher.Entities;
using System.Text.RegularExpressions;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Parses the input pattern, validates it structure and creates 
    /// binary tree of pattern's expressions. 
    /// </summary>
    public class PatternParser
    {
        private Context _context;

        public PatternParser(Context context){
            this._context = context;
        }



        /// <summary>
        /// Parse input query and create tree.
        /// </summary>
        /// <returns>Tree's root.</returns>
        /// <param name="input">Input.</param>
        public void ParseInput(){
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
        private string resolveQueryAsterixOperators(Context context, int tokenFormsMaxNumber){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replace the word with its forms like  word1 -> (word1_1 OR word1_2 OR word1_3) 
        /// (unless it is not in form like =word1 or like "word1 word2"). 
        /// </summary>
        /// <returns>The words forms for token.</returns>
        /// <param name="query">Query.</param>
        private string getWordsFormsForToken(Context context){
            throw new NotImplementedException();
        }
    }
}
