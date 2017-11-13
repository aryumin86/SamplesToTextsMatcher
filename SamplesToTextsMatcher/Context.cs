using System;
using System.Collections.Generic;
using SamplesToTextsMatcher.Entities;
using System.Text.RegularExpressions;
using System.Linq;

namespace SamplesToTextsMatcher
{
    /// <summary>
    /// Input string as tokens array where index is token's position
    /// (Context of interpret operations for terminal and non terminal expressions).
    /// </summary>
    public class Context
    {
        private string _pattern;
        private AbstractMorfDictionary _dict;
        private int _tokenFormsMaxNumberForAsterix;

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

        public Context(string pattern, AbstractMorfDictionary dict, int tokenFormsMaxNumberForAsterix = 30){
            this._pattern = pattern;
            this._dict = dict;
        }

        /// <summary>
        /// Parses input and creates binary tree.
        /// </summary>
        public void ParseInput()
        {
            queryTextFirstFormat();
            validateInput();
            createExpressionsList();
            resolveQueryAsterixOperators();
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
        /// Making expressions linked list.
        /// </summary>
        private void createExpressionsList(){
            this.ExpressionsList = new LinkedList<Expression>();
            char[] charArr = _pattern.ToCharArray();

            for (int i = 0; i < charArr.Length; i++){
                if (charArr[i] == ' ')
                    continue;
                else if(charArr[i] == '('){
                    ExpressionsList.AddLast(new OpeningBracket()
                    {
                        StartIndexAtRaw = i,
                        EndIndexAtRaw = i
                    });

                    continue;
                }
                else if (charArr[i] == ')')
                {
                    ExpressionsList.AddLast(new ClosingBracket()
                    {
                        StartIndexAtRaw = i,
                        EndIndexAtRaw = i
                    });

                    continue;
                }
                else if (charArr[i] == '"')
                {
                    TerminalExpression term = getTerminalExpression(charArr, i);
                    ExpressionsList.AddLast(term);
                    i = term.EndIndexAtRaw;

                    continue;
                }
                else if (charArr[i] == '*')
                {


                    continue;
                }
                else if (charArr[i] == '=')
                {


                    continue;
                }

                NonTerminalExpression exp = getNonTerminal(charArr, i);
                if (exp == null){
                    TerminalExpression term = getTerminalExpression(charArr, i);
                    ExpressionsList.AddLast(term);
                    i = term.EndIndexAtRaw;
                    continue;
                }
                ExpressionsList.AddLast(exp);
                i = exp.EndIndexAtRaw;
            }
        }

        /// <summary>
        /// If it is a start of NonTerminal expression this method will return the
        /// NonTerminal Expression.
        /// </summary>
        /// <returns>The non terminal.</returns>
        /// <param name="arr">Arr.</param>
        /// <param name="startPosition">Start position.</param>
        private NonTerminalExpression getNonTerminal(char[] arr, int startPosition){
            NonTerminalExpression result = null;

            switch(arr[startPosition]){
                case '&':
                    result = new ANDExpression()
                    {
                        StartIndexAtRaw = startPosition
                    };
                    break;

                case '|':
                    result = new ORExpression()
                    {
                        StartIndexAtRaw = startPosition
                    };
                    break;

                case '~':
                    result = new NOTExpression()
                    {
                        StartIndexAtRaw = startPosition
                    };
                    break;

                case '/':
                    int distanseCharLength = 0;
                    while(char.IsDigit(arr[startPosition + 1])){
                        distanseCharLength++;
                    }

                    if (distanseCharLength == 0)
                        throw new FormatException("/n non-terminal format error - no n after /");

                    result = new MaxDistExpression()
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition + 1 + distanseCharLength,
                        N = int.Parse(new string(arr.Skip(startPosition + 1).Take(distanseCharLength).ToArray()))
                    };
                    break;
            }

            return result;
        }

        /// <summary>
        /// Create terminal expression starting from startIndex.
        /// </summary>
        /// <returns>The terminal expression.</returns>
        /// <param name="charArr">Char arr.</param>
        /// <param name="startIndex">Start index.</param>
        private TerminalExpression getTerminalExpression(char[] charArr, int startIndex){
            TerminalExpression term = null;
            int closingQuotesIndex = 0;
            if(charArr[startIndex] == '"'){
                closingQuotesIndex = charArr
                    .Skip(startIndex + 1)
                    .Select((x, y) => new { ch = x, index = y })
                    .First(x => x.ch == '"')
                    .index;

                term = new TerminalExpression(
                    new string(charArr.Skip(startIndex).Take(closingQuotesIndex - startIndex - 1).ToArray()))
                {
                    StartIndexAtRaw = startIndex + 1,
                    EndIndexAtRaw = closingQuotesIndex - 1,
                    InQuotes = true
                };
            }
            else{
                int i = 0;
                while(true){
                    if (charArr[i] == ' ' || charArr[i] == ')' || charArr[i] == '(' || charArr[i] == '|' || charArr[i] == '*' || charArr[i] == '&' || charArr[i] == '~' || charArr[i] == '/'){
                        term = new TerminalExpression(new string(charArr.Skip(startIndex).Take(i).ToArray()))
                        {
                            StartIndexAtRaw = startIndex,
                            EndIndexAtRaw = i-1,
                            InQuotes = false
                        };
                        break;
                    }
                    i++;
                }
            }

            return term;
        }

        /// <summary>
        /// Modifies to inverse polish notaion and makes tree.
        /// </summary>
        private void ModifyToInversePolishAndMakeTree(){
            throw new NotImplementedException();
        }

        /// <summary>
        /// If there are asterix operators in pattern - resolves it - 
        /// appeal to morphological dictionary, gets the forms of words and add terms 
        /// and OR expressions in form like: (form1 | form2 | form3 .... form999).
        /// </summary>
        private void resolveQueryAsterixOperators()
        {
            if (_dict == null)
                return;


        }

        /// <summary>
        /// Replace the word with its forms like  word1 -> (word1_1 OR word1_2 OR word1_3) 
        /// (unless it is not in form like =word1 or like "word1 word2" or ="word1 word2"). 
        /// </summary>
        /// <returns>The words forms for token.</returns>
        private string[] getWordsFormsForTokens(string word)
        {
            string[] result = null;



            return result;
        }
    }
}
