using System;
namespace SamplesToTextsMatcher.Entities
{
    public abstract class NonTerminalExpression : Expression
    {
        /// <summary>
        /// Priority of non-terminal operation.
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }

        public abstract override void Interpret(Context context);
    }
}
