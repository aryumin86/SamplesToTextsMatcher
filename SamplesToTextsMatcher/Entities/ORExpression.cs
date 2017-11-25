using System;
namespace SamplesToTextsMatcher.Entities
{
    public class ORExpression : NonTerminalExpression
    {
        public ORExpression(){
            Priority = 2;
            Raw = " | ";
        }

        public override bool Interpret(Context context)
        {
            if (LeftChild == null)
                throw new FormatException("Can't interpret non-terminal without left child");
            if (RightChild == null)
            {
                throw new FormatException("Can't interpret non-terminal without right child");
            }

            return LeftChild.Interpret(context) | RightChild.Interpret(context);
        }
    }
}
