using System;

namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Terminal or non terminal expression in a tree.
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        /// Non-interpreted expression as string.
        /// </summary>
        /// <value>The raw expression.</value>
        public string Raw { get; set; }

        /// <summary>
        /// Parent node.
        /// </summary>
        /// <value>The parent.</value>
        public Expression Parent { get; set; }

        /// <summary>
        /// Left child node.
        /// </summary>
        /// <value>The left child.</value>
        public Expression LeftChild { get; set; }

        /// <summary>
        /// Right child node.
        /// </summary>
        /// <value>The right child.</value>
        public Expression RightChild { get; set; }

        /// <summary>
        /// Index of first letter of expression in raw pattern string.
        /// </summary>
        /// <value>The start index at raw.</value>
        public int StartIndexAtRaw { get; set; }

        /// <summary>
        /// Index of last letter of expression in raw pattern string.
        /// </summary>
        /// <value>The end index at raw.</value>
        public int EndIndexAtRaw { get; set; }

        public abstract bool Interpret(Context ctx);

        /// <summary>
        /// This array is for recursive interpretation of symtax tree for distance 
        /// operator. If true - this word from raw is actual for interpretation of node.
        /// </summary>
        /// <value>The actual terms at raw.</value>
        public bool[] TermRepresentedInRaw { get; set; }
    }
}
