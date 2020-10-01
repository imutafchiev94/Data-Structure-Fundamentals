using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> _children;

        public Tree(T value)
        {
            this.Value = value;
            this.Parent = null;
            this._children = new List<Tree<T>>();
        }

        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this._children.Add(child);
            }
        }

        public T Value { get; private set; }
        public Tree<T> Parent { get; private set; }
        public IReadOnlyCollection<Tree<T>> Children => this._children.AsReadOnly();
        public bool IsRootDeleted { get; private set; }

        public ICollection<T> OrderBfs()
        {
            var result = new List<T>();

            var queue = new Queue<Tree<T>>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                result.Add(subTree.Value);

                foreach (var chield in subTree.Children)
                {
                    queue.Enqueue(chield);
                }
            }

            return result;
        }

        public ICollection<T> OrderDfs()
        {
            var result = new List<T>();

            if (this.IsRootDeleted)
            {
                return result;
            }

            this.Dfs(this, result);

            return result;
        }

        


        public void AddChild(T parentKey, Tree<T> child)
        {
            var parentSubtree = this.FindBfs(parentKey);
            this.CheckEmptyNode(parentSubtree);

            parentSubtree._children.Add(child);
        }

        
        public void RemoveNode(T nodeKey)
        {
            var currentNode = this.FindBfs(nodeKey);
            this.CheckEmptyNode(currentNode);

            foreach (var chield in currentNode.Children)
            {
                chield.Parent = null;
            }
            currentNode._children.Clear();
            
            var parentNode = currentNode.Parent;
            if (parentNode is null)
            {
                this.IsRootDeleted = true;
            }
            else
            {
                parentNode._children.Remove(currentNode);
            }
            currentNode.Value = default(T);
            
        
            
        }

        public void Swap(T firstKey, T secondKey)
        {
            throw new NotImplementedException();
        }

        private void Dfs(Tree<T> subTree, List<T> result)
        {
            foreach (var chield in subTree.Children)
            {
                this.Dfs(chield, result);
            }

            result.Add(subTree.Value);
        }

        private ICollection<T> OrderDFSWithStack()
        {
            var result = new Stack<T>();

            var stack = new Stack<Tree<T>>();

            stack.Push(this);

            while (stack.Count > 0)
            {
                var subTree = stack.Pop();

                foreach (var chield in subTree.Children)
                {
                    stack.Push(chield);
                }

                result.Push(subTree.Value);
            }

            return new List<T>(result);
        }

        private Tree<T> FindBfs(T value)
        {
            var queue = new Queue<Tree<T>>();

            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                var subTree = queue.Dequeue();
                if (subTree.Value.Equals(value))
                {
                    return subTree;
                }

                foreach (var child in subTree.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return null;
        }
        private void CheckEmptyNode(Tree<T> parentSubtree)
        {
            if (parentSubtree is null)
            {
                throw new ArgumentNullException("Seached node not found!");
            }
        }

        private Tree<T> FindDfs(T value, Tree<T> subTree)
        {
            foreach (var chield in subTree.Children)
            {
                Tree<T> current = this.FindDfs(value, chield);
                if (current != null && current.Value.Equals(value))
                {

                        return current;
                    
                }
            }

            if (subTree.Value.Equals(value))
            {
                return subTree; 
            }

            return null;
        }
    }
}
