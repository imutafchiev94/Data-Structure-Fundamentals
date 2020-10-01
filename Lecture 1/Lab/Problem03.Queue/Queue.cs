using System.ComponentModel.Design;
using System.Linq;

namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private Node<T> _head;


        public int Count { get; private set; }

        public bool Contains(T item)
        {
            var current = this._head;

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

        public T Dequeue()
        {
            EnsureNotEmpty();

            Node<T> current = this._head;
            this._head = this._head.Next;
            this.Count--;
            return current.Element;
        }

        public void Enqueue(T item)
        {
            Node<T> toInsert = new Node<T>
            {
                Element = item,
                Next = null
            };

            if (this._head == null)
            {
                this._head = toInsert;
            }
            else
            {
                var current = this._head;

                while (current.Next != null)
                {
                    current = current.Next;
                }

                current.Next = toInsert;
            }
            this.Count++;
        }



        public T Peek()
        {
            EnsureNotEmpty();
            return this._head.Element;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;

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
                throw new InvalidOperationException("The Queue is Empty");
            }
        }
    }
}