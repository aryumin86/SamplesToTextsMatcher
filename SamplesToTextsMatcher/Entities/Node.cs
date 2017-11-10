using System;
namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Node of a tree. Contains the expression that should be interpreted.
    /// It can be terminal or non-terminal expression.
    /// </summary>
    public class Node
    {
        public Node LeftChild { get; set; }

        public Node RightChild { get; set; }

        /// <summary>
        /// Terminal or Non-terminal expression that node contains.
        /// </summary>
        /// <value>The expression.</value>
        public Expression Expression { get; set; }

        /// <summary>
        /// Index of first symbol in full query.
        /// </summary>
        /// <value>The start.</value>
        public int Start { get; set; }

        /// <summary>
        /// Index of last symbol in full query.
        /// </summary>
        /// <value>The end.</value>
        public int End { get; set; }
    }
}
