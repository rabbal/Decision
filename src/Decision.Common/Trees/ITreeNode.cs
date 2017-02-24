using System.Collections.Generic;

namespace Decision.Common.Trees
{
    /// <summary>
    ///     An object which implements this interface is considered a node in a tree.
    /// </summary>
    public interface ITreeNode<T>
        where T : class
    {
        /// <summary>
        ///     A unique identifier for the node.
        /// </summary>
        int Id { get; }

        /// <summary>
        ///     The parent of this node, or null if it is the root of the tree.
        /// </summary>
        T Parent { get; set; }

        /// <summary>
        ///     The children of this node, or an empty list if this is a leaf.
        /// </summary>
        IList<T> Children { get; set; }
    }
}