using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Decision.Framework.Extensions;
using Decision.Framework.GuardToolkit;
using Decision.Framework.Infrastructure;

namespace Decision.Framework.Collections
{
    public class TreeNode<T> : ICloneable<TreeNode<T>>
    {
        private readonly LinkedList<TreeNode<T>> _children = new LinkedList<TreeNode<T>>();
        private int? _depth;

        public TreeNode(T value)
        {
            Value = value;
        }

        #region Properties

        public TreeNode<T> Parent { get; private set; }

        public T Value { get; set; }

        public TreeNode<T> this[int i] => _children.ElementAt(i);

        public IEnumerable<TreeNode<T>> Children => _children;

        public IEnumerable<TreeNode<T>> LeafNodes
        {
            get { return _children.Where(x => x.IsLeaf); }
        }

        public IEnumerable<TreeNode<T>> NonLeafNodes
        {
            get { return _children.Where(x => !x.IsLeaf); }
        }


        public TreeNode<T> FirstChild
        {
            get
            {
                var first = _children.First;
                return first?.Value;
            }
        }

        public TreeNode<T> LastChild
        {
            get
            {
                var last = _children.Last;
                return last?.Value;
            }
        }

        public bool IsLeaf => _children.Count == 0;

        public bool HasChildren => _children.Count > 0;

        public bool IsRoot => Parent == null;

        public int Depth
        {
            get
            {
                if (!_depth.HasValue)
                {
                    var node = this;
                    var depth = -1;
                    while (node != null && !node.IsRoot)
                    {
                        depth++;
                        node = node.Parent;
                    }
                    _depth = depth;
                }

                return _depth.Value;
            }
        }

        public TreeNode<T> Root
        {
            get
            {
                var root = this;
                while (root.Parent != null)
                {
                    root = root.Parent;
                }
                return root;
            }
        }

        public TreeNode<T> Next
        {
            get
            {
                if (Parent == null) return null;

                var self = Parent._children.Find(this);
                var next = self?.Next;
                return next?.Value;
            }
        }

        public TreeNode<T> Previous
        {
            get
            {
                if (Parent != null)
                {
                    var self = Parent._children.Find(this);
                    var prev = self != null ? self.Previous : null;
                    if (prev != null)
                        return prev.Value;
                }
                return null;
            }
        }

        #endregion

        #region Methods/Declarations

        private void AddChild(TreeNode<T> node, bool clone, bool append = true)
        {
            var newNode = node;
            if (clone)
            {
                newNode = node.Clone(true);
            }
            newNode.Parent = this;
            newNode.TraverseTree(x => x._depth = null);
            if (append)
            {
                _children.AddLast(newNode);
            }
            else
            {
                _children.AddFirst(newNode);
            }
        }

        #region Append

        public TreeNode<T> Append(T value)
        {
            var node = new TreeNode<T>(value);
            AddChild(node, false);
            return node;
        }

        public void Append(TreeNode<T> node, bool clone = true)
        {
            AddChild(node, clone);
        }

        public ICollection<TreeNode<T>> AppendMany(IEnumerable<T> values)
        {
            return values.Select(Append).AsReadOnly();
        }

        public TreeNode<T>[] AppendMany(params T[] values)
        {
            return values.Select(Append).ToArray();
        }

        public void AppendMany(IEnumerable<TreeNode<T>> values)
        {
            values.Each(x => AddChild(x, true));
        }

        public void AppendChildrenOf(TreeNode<T> node)
        {
            node._children.Each(x => AddChild(x, true));
        }

        #endregion

        #region Prepend

        public TreeNode<T> Prepend(T value)
        {
            var node = new TreeNode<T>(value);
            AddChild(node, true, false);
            return node;
        }

        #endregion

        #region Insert[...]

        public void InsertAfter(TreeNode<T> refNode)
        {
            Insert(refNode, true);
        }

        public void InsertBefore(TreeNode<T> refNode)
        {
            Insert(refNode, false);
        }

        private void Insert(TreeNode<T> refNode, bool after)
        {
            Check.ArgumentNotNull(() => refNode);

            if (refNode.Parent == null)
            {
                throw Error.Argument($"refNode",
                    "The reference node cannot be a root node and must be attached to the tree.");
            }

            var refLinkedList = refNode.Parent._children;
            var refNodeInternal = refLinkedList.Find(refNode);

            if (Parent != null)
            {
                var thisLinkedList = Parent._children;
                thisLinkedList.Remove(this);
            }

            if (after)
            {
                refLinkedList.AddAfter(refNodeInternal, this);
            }
            else
            {
                refLinkedList.AddBefore(refNodeInternal, this);
            }

            Parent = refNode.Parent;
            TraverseTree(x => x._depth = null);
        }

        #endregion

        #region Select[...]

        public TreeNode<T> SelectNode(Expression<Func<TreeNode<T>, bool>> predicate)
        {
            Check.ArgumentNotNull(() => predicate);

            return FlattenNodes(predicate, false).FirstOrDefault();
        }

        /// <summary>
        ///     Selects all nodes (recursively) with match the given <c>predicate</c>,
        ///     but excluding self
        /// </summary>
        /// <param name="predicate">The predicate to match against</param>
        /// <returns>A readonly collection of node matches</returns>
        public ICollection<TreeNode<T>> SelectNodes(Expression<Func<TreeNode<T>, bool>> predicate)
        {
            Check.ArgumentNotNull(() => predicate);

            var result = new List<TreeNode<T>>();

            var flattened = FlattenNodes(predicate, false);
            result.AddRange(flattened);

            return result.AsReadOnly();
        }

        #endregion

        public bool RemoveNode(TreeNode<T> node)
        {
            node.TraverseTree(x => x._depth = null);
            return _children.Remove(node);
        }


        public void Clear()
        {
            _children.Clear();
        }

        public void Traverse(Action<T> action)
        {
            if (Value != null) action?.Invoke(Value);

            foreach (var child in _children)
                child.Traverse(action);
        }

        public void TraverseTree(Action<TreeNode<T>> action)
        {
            action?.Invoke(this);
            foreach (var child in _children)
                child.TraverseTree(action);
        }

        public IEnumerable<T> Flatten(bool includeSelf = true)
        {
            return Flatten(null, includeSelf);
        }

        public IEnumerable<T> Flatten(Expression<Func<T, bool>> expression, bool includeSelf = true)
        {
            var list = includeSelf ? new[] {Value} : Enumerable.Empty<T>();

            var result = list.Union(_children.SelectMany(x => x.Flatten()));
            if (expression != null)
            {
                result = result.Where(expression.Compile());
            }

            return result;
        }

        internal IEnumerable<TreeNode<T>> FlattenNodes(bool includeSelf = true)
        {
            return FlattenNodes(null, includeSelf);
        }

        internal IEnumerable<TreeNode<T>> FlattenNodes(Expression<Func<TreeNode<T>, bool>> expression,
            bool includeSelf = true)
        {
            var list = includeSelf ? new[] {this} : Enumerable.Empty<TreeNode<T>>();

            var result = list.Union(_children.SelectMany(x => x.FlattenNodes()));
            if (expression != null)
            {
                result = result.Where(expression.Compile());
            }

            return result;
        }

        public TreeNode<T> Find(T value)
        {
            //Check.ArgumentNotNull(value, "value"); 

            if (Value.Equals(value))
            {
                return this;
            }

            TreeNode<T> item = null;

            foreach (var child in _children)
            {
                item = child.Find(value);
                if (item != null)
                    break;
            }

            return item;
        }

        public TreeNode<T> Clone()
        {
            return Clone(true);
        }

        public TreeNode<T> Clone(bool deep)
        {
            var value = Value;

            if (value is ICloneable)
            {
                value = (T) ((ICloneable) value).Clone();
            }

            var clone = new TreeNode<T>(value);
            if (deep)
            {
                clone.AppendChildrenOf(this);
            }
            return clone;
        }

        object ICloneable.Clone()
        {
            return Clone(true);
        }

        #endregion
    }
}