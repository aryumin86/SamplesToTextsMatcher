using System;
using System.Linq;

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

            TermRepresentedInRaw =
                Enumerable.Repeat<bool>(false, context.CurrentStringToMatchWithTree.Length)
                          .ToArray();

            //moving both sides from left child term (for each term left from previos interpretations)  
            //trying 1. to find any match for two terms staying near (near = <= N)
            //and 2. to identifing terms from raw string which will be used for 
            //consequent interpretations
            for (int i = 0; i < context.CurrentStringToMatchWithTree.Length; i++){

                if (LeftChild.TermRepresentedInRaw[i] == false)
                    continue;

                int pacesDoneCount = 0;

                //to left moves within N distance
                for (int j = i-1; j >= 0; --j){
                    if(RightChild.TermRepresentedInRaw[j] == true){
                        TermRepresentedInRaw[j] = true;
                        res = true;
                    }

                    pacesDoneCount++;
                    if (pacesDoneCount > N)
                        break;
                }

                pacesDoneCount = 0;

                //to rights moves within N distance
                for (int j = i+1; j < context.CurrentStringToMatchWithTree.Length; ++j)
                {
                    if (RightChild.TermRepresentedInRaw[j] == true)
                    {
                        TermRepresentedInRaw[j] = true;
                        res = true;
                    }

                    pacesDoneCount++;
                    if (pacesDoneCount > N)
                        break;
                }

            }

            return res;
        }
    }
}
