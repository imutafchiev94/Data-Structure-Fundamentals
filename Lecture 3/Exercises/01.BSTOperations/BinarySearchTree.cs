namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            this.Coppy(root);
        }

        private void Coppy(Node<T> root)
        {
            if (root != null)
            {
                this.Insert(root.Value);
                this.Coppy(root.LeftChild);
                this.Coppy(root.RightChild);
            }
        }

        public Node<T> Root { get; private set; }

        public Node<T> LeftChild { get; private set; }

        public Node<T> RightChild { get; private set; }

        public T Value => this.Root.Value;

        public int Count => throw new NotImplementedException();

        public bool Contains(T element)
        {
            var current = this.Root;

            while (current != null)
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if (this.IsGreater(element, current.Value))
                {
                    current = current.RightChild;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            var toInsert = new Node<T>(element, null, null);
            if (this.Root == null)
            {
                this.Root = toInsert;
            }
            else
            {
                var current = this.Root;
                Node<T> prev = null;

                while (current != null)
                {
                    prev = current;
                    if (this.IsLess(element, current.Value))
                    {
                        current = current.LeftChild;
                    }
                    else if (this.IsGreater(element, current.Value))
                    {
                        current = current.RightChild;
                    }
                    else
                    {
                        return;
                    }
                }

                if (this.IsLess(element, prev.Value))
                    {
                        prev.LeftChild = toInsert;
                        if (this.LeftChild == null)
                        {
                            this.LeftChild = toInsert;
                        }
                    }
                    else if (this.IsGreater(element, prev.Value))
                    {
                        prev.RightChild = toInsert;
                        if (this.RightChild == null)
                        {
                            this.RightChild = toInsert;
                        }
                    }
                
            }
        }

        

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var current = this.Root;

            while (current != null && !this.AreEqual(element, current.Value))
            {
                if (this.IsLess(element, current.Value))
                {
                    current = current.LeftChild;
                }
                else if(IsGreater(element, current.Value))
                {
                    current = this.RightChild;
                }
            }

            return new BinarySearchTree<T>(current);
        }

        public void EachInOrder(Action<T> action)
        {
            if (this.LeftChild != null)
            {
                var leftTree = new BinarySearchTree<T>(this.LeftChild);
                leftTree.EachInOrder(action);
            }
            action.Invoke(this.Value);
            if (this.RightChild != null)
            {
                var rightTree = new BinarySearchTree<T>(this.RightChild);
                rightTree.EachInOrder(action);
            }
        }

        public List<T> Range(T lower, T upper)
        {
            throw new NotImplementedException();
        }

        public void DeleteMin()
        {
            this.IsNotEmpty();
            var current = this.Root;
            Node<T> prev = null;
            while (current != null && current.LeftChild != null)
            {
                prev = current;
                current = current.LeftChild;
            }

            current = null;
            prev.LeftChild = prev.RightChild;
        }

        private void IsNotEmpty()
        {
            if (this.Root == null)
            {
                throw new InvalidOperationException("The BST is empty");
            }
        }

        public void DeleteMax()
        {
            this.IsNotEmpty();
            var current = this.Root;
            Node<T> leftChield = null;
            Node<T> prev = null;
            while (current != null)
            {
                prev = current;
                
                current = current.RightChild;
                
            }

            current = null;
            prev.RightChild = null;
        }

        public int GetRank(T element)
        {
            throw new NotImplementedException();
        }

        private bool IsLess(T element, T currentValue)
        {
            return element.CompareTo(currentValue) < 0;
        }
        private bool IsGreater(T element, T currentValue)
        {
            return element.CompareTo(currentValue) > 0;
        }
        private bool AreEqual(T element, T currentValue)
        {
            return element.CompareTo(currentValue) == 0;
        }
    }
}
