using System.Linq;
using System.Reflection;
using System.Text;

namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this._children = new List<Tree<T>>();

            foreach (var chield in children)
            {
                this.AddChild(chield);
                chield.Parent = this;
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }


        public IReadOnlyCollection<Tree<T>> Children
            => this._children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this._children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string GetAsString()
        {
            StringBuilder result = new StringBuilder();
            this.OrderDfsForString(0, result, this);

            return result.ToString().Trim();
        }

        

        public Tree<T> GetDeepestLeftomostNode()
        {
            var depthNode = new Dictionary<int, Tree<T>>();
            depthNode.Add(0, this);

            OrderDfs(0, depthNode, this);

            return depthNode.Values.Last();
        }

        public List<T> GetLeafKeys()
        {
            Func<Tree<T>, bool> leafKeysPredicat = (node) => (IsLeaf(node));
            List<T> leafKeys = new List<T>();
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var current = nodes.Dequeue();

                if (this.IsLeaf(current))
                {
                    leafKeys.Add(current.Key);
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        nodes.Enqueue(child);
                    }
                }
            }

            return leafKeys;
        }

        

        public List<T> GetMiddleKeys()
        {
            List<T> middleKeys = new List<T>();
            Queue<Tree<T>> nodes = new Queue<Tree<T>>();
            nodes.Enqueue(this);

            while (nodes.Count > 0)
            {
                var current = nodes.Dequeue();

                if (this.IsMiddle(current))
                {
                    middleKeys.Add(current.Key);
                }
                else
                {
                    foreach (var child in current.Children)
                    {
                        nodes.Enqueue(child);
                    }
                }
            }

            return middleKeys;
        }

        public List<T> GetLongestPath()
        {
            var depthNode = new Dictionary<int, Tree<T>>();
            depthNode.Add(0, this);

            OrderDfs(0, depthNode, this);

            List<T> path = new List<T>();

            foreach (var node in depthNode.Values)
            {
                path.Add(node.Key);
            }

            return path;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            throw new NotImplementedException();
        }

        private void OrderDfsForString(int depth, StringBuilder result, Tree<T> subtree)
        {
            result.Append(new string(' ', depth))
                .Append(subtree.Key)
                .Append(Environment.NewLine);

            foreach (var child in subtree.Children)
            {
                this.OrderDfsForString(depth + 2, result, child);

            }
        }

        private void OrderDfs(int depth, Dictionary<int, Tree<T>> depthNode, Tree<T> subTree)
        {
            depth++;
            foreach (var child in subTree.Children)
            {
                if (depthNode.Keys.Last() < depth)
                {
                    depthNode[depth] = child;
                }

                OrderDfs(depth, depthNode, child);
            }
        }

        private bool IsLeaf(Tree<T> subtree)
        {
            if (subtree.Children.Count == 0)
            {
                return true;
            }
            return false;
        }

        private bool IsParent(Tree<T> subtree)
        {
            if (subtree.Children.Count != 0)
            {
                return true;
            }
            return false;
        }

        private bool IsMiddle(Tree<T> subtree)
        {
            if (IsParent(subtree) && subtree.Parent != null)
            {
                return true;
            }
            return false;
        }

        private void OrderDfsToList(int depth, Stack<Tree<T>> depthNode, Tree<T> subTree)
        {
            depth++;
            foreach (var child in subTree.Children)
            {
                if(depthNode.Count <= depth && !IsLeaf(child))
                { depthNode.Push(child); }
                
                if (depthNode.Count <= depth && IsLeaf(child))
                {
                    depthNode.Push(child);
                }

                OrderDfsToList(depth, depthNode, child);
            }
        }
    }
}
