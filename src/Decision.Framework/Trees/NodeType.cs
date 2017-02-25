namespace Decision.Framework.Trees
{
    /// <summary>
    ///     A type of tree node.
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        ///     A node which is at the root of the tree, i.e. it has no parents.
        /// </summary>
        Root,

        /// <summary>
        ///     A node which has parent and children.
        /// </summary>
        Internal,

        /// <summary>
        ///     A node with no children.
        /// </summary>
        Leaf
    }
}