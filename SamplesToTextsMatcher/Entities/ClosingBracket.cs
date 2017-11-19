using System;
using System.Collections.Generic;
using System.Text;

namespace SamplesToTextsMatcher.Entities
{
    public class ClosingBracket : NonTerminalExpression
    {
        public ClosingBracket(){
            Priority = 1;
        }

        public override void Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
