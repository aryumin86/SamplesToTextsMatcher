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
        private string _pattern;

        /// <summary>
        /// Queue of expressions got from input.
        /// </summary>
        /// <value>The expressions queue.</value>
        public LinkedList<Expression> ExpressionsList { get; set; }

        /// <summary>
        /// Root of expressions binary tree.
        /// </summary>
        /// <value>The root.</value>
        public Expression Root { get; set; }

        public Context(string pattern){
            this._pattern = pattern;
        }

        /// <summary>
        /// Parses input and creates binary tree.
        /// </summary>
        public void ParseInput()
        {
            queryTextFirstFormat();
            validateInput();
            createExpressionsQueue();
            ModifyToInversePolishAndMakeTree();
        }

        /// <summary>
        /// Matches string to pattern.
        /// </summary>
        /// <returns><c>true</c>, if pattern to string was matched, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
        public bool MatchPatternToString(string input){
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes double spaces, make to lower case the pattern string etc.
        /// </summary>
        /// <returns>The text first format.</returns>
        /// <param name="query">Query.</param>
        private void queryTextFirstFormat()
        {
            this._pattern = this._pattern.ToLower().Trim('\n','\r', ' ');
            this._pattern = Regex.Replace(this._pattern, @"\s+", " ");
        }

        /// <summary>
        /// Validate if a query string is logically valid.
        /// </summary>
        /// <param name="context">context.</param>
        private void validateInput()
        {
            if (string.IsNullOrWhiteSpace(_pattern))
                throw new FormatException("Input validation error. Pattern is empty");

            Stack<char> quotesStack = new Stack<char>();
            Stack<char> bracketsStack = new Stack<char>();

            var inputArr = _pattern.ToCharArray();
            for (int i = 0; i < inputArr.Length; i++){
                if(inputArr[i] == '('){
                    bracketsStack.Push('(');
                }
                else if(inputArr[i] == ')'){
                    if (bracketsStack.Count == 0)
                        throw new FormatException("Pattern validation error. Brackets are used in a wrong way");
                }
                else if(inputArr[i] == '"'){
                    quotesStack.Push('"');
                }
                else if(inputArr[i] == '*'){
                    if (i == 0)
                        throw new FormatException("Pattern validation error. Asterix can't be at the very beginning of the pattern");
                    if (!char.IsLetter(inputArr[i - 1]))
                        throw new FormatException("Pattern validation error. Asterix can be only at the end of the term");
                }
                else if(inputArr[i] == '='){
                    if (i == inputArr.Length - 1 || inputArr[i + 1] != '"' || !char.IsLetter(inputArr[i + 1]))
                        throw new FormatException("Pattern validation error. Equals sign can be only before letter or quotes sign");
                    
                }
            }

            if (quotesStack.Count > 0 && quotesStack.Count % 2 != 0)
                throw new FormatException("Pattern validation error. No closing quotes in pair of quotes");
        }


        /// <summary>
        /// Making expressions queue.
        /// </summary>
        private void createExpressionsQueue(){
            this.ExpressionsList = new LinkedList<Expression>();

            throw new NotImplementedException();
        }

        /// <summary>
        /// Modifies to inverse polish notaion and makes tree.
        /// </summary>
        private void ModifyToInversePolishAndMakeTree(){
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
        private string resolveQueryAsterixOperators(Context context, int tokenFormsMaxNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Replace the word with its forms like  word1 -> (word1_1 OR word1_2 OR word1_3) 
        /// (unless it is not in form like =word1 or like "word1 word2" or ="word1 word2"). 
        /// </summary>
        /// <returns>The words forms for token.</returns>
        private void getWordsFormsForTokens(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
