namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private Node<T> _top;

        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._top;

            while (current != null)
            {
                if (current.Element.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public T Peek()
        {
            EnsureNotEmpty();
            return this._top.Element;
        }

        public T Pop()
        {
            EnsureNotEmpty();
            var current = this._top.Element;
            var nextTopElement = this._top.Next;
            this._top.Next = null;
            this._top = nextTopElement;
            this.Count--;

            return current;
        }

        
        public void Push(T item)
        {
            var newNode = new Node<T>
            {
                Element = item,
                Next = this._top
            };

            this._top = newNode;
            this.Count++;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._top;

            while (current != null)
            {
                yield return current.Element;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();

        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException("The Stack is empty");
            }
        }

    }
}