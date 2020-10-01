namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private Node<T> _head;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> toInsert = new Node<T>
            {
                Value = item,
                Next = this._head,
                Previous = null
            };

            this._head = toInsert;
            if (this._head.Next != null)
            {
                this._head.Next.Previous = this._head;
            }
            this.Count++;
        }

        public void AddLast(T item)
        {
            Node<T> toInsert = new Node<T>
            {
                Value = item,
                Next = null,
                Previous = _head
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

        public T GetFirst()
        {
            EnsureNotEmpty();

            return this._head.Value;
        }

       
        public T GetLast()
        {
            EnsureNotEmpty();

            var current = this._head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Value;
        }

        public T RemoveFirst()
        {
            EnsureNotEmpty();

            Node<T> current = _head;
            this._head = this._head.Next;
            this.Count--;
            return current.Value;
        }

        public T RemoveLast()
        {
            EnsureNotEmpty();

            while (this._head.Next != null)
            {
                this._head = this._head.Next;
            }

            var current = this._head.Value;
            if (this._head.Previous != null)
            {
                this._head = this._head.Previous;
            }
            this._head.Next = null;
            this.Count--;
            return current;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = this._head;

            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw  new InvalidOperationException($"The List is Empty");
            }
        }
    }
}