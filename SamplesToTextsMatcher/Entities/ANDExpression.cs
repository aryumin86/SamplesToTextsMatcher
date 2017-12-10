using System;
using System.Linq;

namespace SamplesToTextsMatcher.Entities
{
    public class ANDExpression : NonTerminalExpression
    {
        public ANDExpression(){
            Priority = 3;
            Raw = " & ";
        }

        public override bool Interpret(Context context)
        {
            if (LeftChild == null)
                throw new FormatException("Can't interpret non-terminal without left child");
            if(RightChild == null){
                throw new FormatException("Can't interpret non-terminal without right child");
            }


            var l = LeftChild.Interpret(context);
            var r = RightChild.Interpret(context);
            var res = l && r;

            ResStringExpression = string.Format("({0} & {1})",
                LeftChild.ResStringExpression, RightChild.ResStringExpression);

            TermRepresentedInRaw =
                Enumerable.Repeat<bool>(false, context.CurrentStringToMatchWithTree.Length)
                          .ToArray();

            for (int i = 0; i < TermRepresentedInRaw.Length; i++)
            {
                if (LeftChild.TermRepresentedInRaw[i] == true && RightChild.TermRepresentedInRaw[i] == true)
                    TermRepresentedInRaw[i] = true;
            }

            return res;
        }
    }
}
