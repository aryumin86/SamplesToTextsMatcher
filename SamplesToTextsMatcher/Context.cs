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
        /// <summary>
        /// Id of context.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// This context is a part of contexts groups with this Id.
        /// </summary>
        public int GroupId { get; set; }

        private string _pattern;
        private AbstractMorfDictionary _dict;
        //TODO It should be got from config file.
        private int _tokenFormsMaxNumberForAsterix = 30;

        /// <summary>
        /// Queue of expressions got from input.
        /// </summary>
        /// <value>The expressions queue.</value>
        public LinkedList<Expression> ExpressionsList { get; set; }

        /// <summary>
        /// This is the result queue in inverse polish nitation.
        /// </summary>
        public Queue<Expression> InversedPolishQueue { get; set; }

        /// <summary>
        /// Gets or sets the current string to match with tree.
        /// It is represented as strings array.
        /// </summary>
        /// <value>The current string to match with tree.</value>
        public string[] CurrentStringToMatchWithTree { get; set; }

        /// <summary>
        /// Root of expressions binary tree.
        /// </summary>
        /// <value>The root.</value>
        public Expression Root { get; set; }

        /// <summary>
        /// Main constructor.
        /// </summary>
        /// <param name="pattern"></param>
        /// <param name="dict"></param>
        /// <param name="shouldWorkWithTermsForms">for example if pattern is from db 
        /// - all forms are already in pattern and no need for them</param>
        public Context(string pattern, AbstractMorfDictionary dict, bool shouldWorkWithTermsForms){
            this._pattern = pattern;
            this._dict = dict;
            InversedPolishQueue = new Queue<Expression>();

            queryTextFirstFormat();
            validateInput();
            createExpressionsList();
            ResolveAllEqualsSigns();
            getWordsFormsForTokens();
            resolveQueryAsterixOperators();
            ModifyToInversePolish();
            CreateTreeFromQueue();
        }

        /// <summary>
        /// Matches string to pattern.
        /// </summary>
        /// <returns><c>true</c>, if pattern to string was matched, <c>false</c> otherwise.</returns>
        /// <param name="input">Input.</param>
        public bool MatchPatternToString(string[] input){
            CurrentStringToMatchWithTree = input;
            return Root.Interpret(this);
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
                    if (i == inputArr.Length - 1 || (inputArr[i + 1] != '"' && !char.IsLetter(inputArr[i + 1])))
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
                    if (ExpressionsList.Last() == null || ExpressionsList.Last().GetType() != typeof(TerminalExpression))
                        throw new FormatException("Asterix should be only at the end of term");
                    else
                        ((TerminalExpression)ExpressionsList.Last()).HasAsterixSign = true;

                    continue;
                }
                else if (charArr[i] == '=')
                {
                    ExpressionsList.AddLast(new EqualsSign());
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
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition
                    };
                    break;

                case '|':
                    result = new ORExpression()
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition
                    };
                    break;

                case '~':
                    result = new NOTExpression()
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition
                    };
                    break;

                case '/':
                    int distanseCharLength = 0;
                    while(char.IsDigit(arr[startPosition + distanseCharLength + 1])){
                        distanseCharLength++;
                    }

                    if (distanseCharLength == 0)
                        throw new FormatException("/n non-terminal format error - no n after /");

                    result = new MaxDistExpression(int.Parse(new string(arr.Skip(startPosition + 1).Take(distanseCharLength).ToArray())))
                    {
                        StartIndexAtRaw = startPosition,
                        EndIndexAtRaw = startPosition + distanseCharLength
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
                    .index + 1 + startIndex;

                term = new TerminalExpression(
                    "\"" + new string(charArr.Skip(startIndex + 1).Take(closingQuotesIndex - startIndex - 1).ToArray()) + "\"")
                {
                    StartIndexAtRaw = startIndex,
                    EndIndexAtRaw = closingQuotesIndex,
                    InQuotes = true
                };
            }
            else{
                int i = startIndex;
                while(true){
                    if (charArr.Length == i || charArr[i] == ' ' || charArr[i] == ')' || charArr[i] == '(' || charArr[i] == '|' || charArr[i] == '*' || charArr[i] == '&' || charArr[i] == '~' || charArr[i] == '/'){
                        term = new TerminalExpression(new string(charArr.Skip(startIndex).Take(i - startIndex).ToArray()))
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
        /// Modifies to inverses polish notaion and makes tree.
        /// </summary>
        private void ModifyToInversePolish(){
            Stack<Expression> stack = new Stack<Expression>();

            var arr = ExpressionsList.ToArray();
            for (int i = 0; i < arr.Length; i++){

                //delete this!!!!
                var elem = arr[i];

                if(arr[i] is TerminalExpression){
                    InversedPolishQueue.Enqueue(arr[i]);
                }
                else if(arr[i] is OpeningBracket){
                    stack.Push(arr[i]);
                }
                else if(arr[i] is ClosingBracket){
                    while (true)
                    {
                        Expression exp = stack.Pop();
                        if (exp is OpeningBracket)
                            break;
                        InversedPolishQueue.Enqueue(exp);
                        if (!stack.Any())
                            throw new Exception("No closing bracket for opened one");
                    }
                }
                else if(arr[i] is NonTerminalExpression){
                    
                    while (stack.Any() && ((NonTerminalExpression)stack.Peek()).Priority >= ((NonTerminalExpression)arr[i]).Priority)
                    {
                        InversedPolishQueue.Enqueue(stack.Pop());
                    }
                    stack.Push(arr[i]);
                }
            }

            while (stack.Any())
                InversedPolishQueue.Enqueue(stack.Pop());
        }

        /// <summary>
        /// Creates the tree from queue.
        /// </summary>
        private void CreateTreeFromQueue(){
            Queue<Expression> qu = new Queue<Expression>(InversedPolishQueue);
            Stack<Expression> temp = new Stack<Expression>();

            while(qu.Any()){
                var z = qu.Dequeue();
                if (z is NonTerminalExpression)
                {
                    var exp1 = temp.Pop();
                    var exp2 = temp.Pop();
                    z.LeftChild = exp2;
                    z.RightChild = exp1;
                    z.LeftChild.Parent = z;
                    z.RightChild.Parent = z;

                    Root = z;
                }

                temp.Push(z);
            }

            //if there is only terminal in whole expression. ??????????????
            if (temp.Any())
                Root = temp.Pop();
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
        private void getWordsFormsForTokens()
        {
            if (_dict == null)
                return;
        }

        /// <summary>
        /// If epression is equals sign it updates the term's property NeedsExactForm after it
        /// and removes this object from list.
        /// </summary>
        private void ResolveAllEqualsSigns(){
            bool nextTermShouldBeInExactForm = false;
            int count = 0;
            foreach(var exp in ExpressionsList){
                if(nextTermShouldBeInExactForm){
                    ((TerminalExpression)exp).NeedsExactForm = true;
                    nextTermShouldBeInExactForm = false;
                }
                count++;
                if(exp is EqualsSign){
                    if (count == ExpressionsList.Count())
                        throw new FormatException("equals sign can't be the last expression in a pattern");
                    nextTermShouldBeInExactForm = true;
                    continue;
                }
                nextTermShouldBeInExactForm = false;
            }
        }
    }
}
