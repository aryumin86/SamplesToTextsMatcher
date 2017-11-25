using System;
namespace SamplesToTextsMatcher.Entities
{
    public class NOTExpression : NonTerminalExpression
    {
        public NOTExpression(){
            Priority = 3;
            Raw = " ~ ";
        }

        public override bool Interpret(Context context)
        {
            if (LeftChild == null)
                throw new FormatException("Can't interpret non-terminal without left child");
            if (RightChild == null)
            {
                throw new FormatException("Can't interpret non-terminal without right child");
            }

            return !(LeftChild.Interpret(context) && RightChild.Interpret(context));
        }
    }
}
