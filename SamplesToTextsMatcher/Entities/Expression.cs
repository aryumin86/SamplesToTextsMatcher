using System;
namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Terminal or non terminal expression in a tree.
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        /// Boolean result of expression's interpretation.
        /// If not Interpreted - it is null.
        /// </summary>
        /// <value>The interpretation result.</value>
        public bool? Result { get; set; }

        /// <summary>
        /// Non-interpreted expression as string.
        /// </summary>
        /// <value>The raw expression.</value>
        public string RawExpression { get; set; }

        public abstract void Interpret(Context context);
    }
}
