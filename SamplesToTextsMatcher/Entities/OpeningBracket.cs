using System;
using System.Collections.Generic;
using System.Text;

namespace SamplesToTextsMatcher.Entities
{
    public class OpeningBracket : NonTerminalExpression
    {
        public OpeningBracket(){
            Priority = 1;
            Raw = "(";
        }

        public override bool Interpret(Context context)
        {
            throw new NotImplementedException();
        }
    }
}
