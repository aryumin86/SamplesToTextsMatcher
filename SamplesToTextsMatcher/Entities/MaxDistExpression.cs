using System;
namespace SamplesToTextsMatcher.Entities
{
    public class MaxDistExpression : NonTerminalExpression
    {
        /// <summary>
        /// N between term 1 and term 2 (in A /n B expression).
        /// </summary>
        /// <value>The n.</value>
        public int N { get; set; }

        public MaxDistExpression(int n){
            Priority = 3;
            Raw = "/" + n;
            N = n;
        }

        public override bool Interpret(Context context)
        {
            if (LeftChild == null)
                throw new FormatException("Can't interpret non-terminal without left child");
            if (RightChild == null)
            {
                throw new FormatException("Can't interpret non-terminal without right child");
            }

            bool leftInterpetation = LeftChild.Interpret(context);
            bool rightInterpretation = RightChild.Interpret(context);

            if (!leftInterpetation || !rightInterpretation)
                return false;

            bool res = false;

            //moving both sides from left child term (for each term left from previos interpretations)  
            //trying 1. to find any match for two terms staying near (near = <= N)
            //and 2. to identifing terms from raw string which will be used for 
            //consequent interpretations
            for (int i = 0; i < context.CurrentStringToMatchWithTree.Length; i++){
                
            }

            return res;
        }
    }
}
