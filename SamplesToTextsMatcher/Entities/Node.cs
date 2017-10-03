using System;
namespace SamplesToTextsMatcher.Entities
{
    /// <summary>
    /// Node of a tree.
    /// </summary>
    public class Node
    {
        public Node LeftChild { get; set; }

        public Node RightChild { get; set; }

        public Expression Expression { get; set; }
    }
}
