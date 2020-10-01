namespace Problem02.DoublyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class DoublyLinkedList<T> : IAbstractLinkedList<T>
    {

        private Node<T> _head;
        private Node<T> _tail;

        public int Count { get; private set; }

        public void AddFirst(T item)
        {
            Node<T> toInsert = new Node<T>
            {
                Item = item,
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
                Item = item,
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

            return this._head.Item;
        }


        public T GetLast()
        {
            EnsureNotEmpty();

            var current = this._head;

            while (current.Next != null)
            {
                current = current.Next;
            }

            return current.Item;
        }

        public T RemoveFirst()
        {
            EnsureNotEmpty();

            Node<T> current = _head;
            this._head = this._head.Next;
            this.Count--;
            return current.Item;
        }

        public T RemoveLast()
        {
            EnsureNotEmpty();

            while (this._head.Next != null)
            {
                this._head = this._head.Next;
            }

            var current = this._head.Item;
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
                yield return current.Item;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();
        private void EnsureNotEmpty()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException($"The List is Empty");
            }
        }

        //private Node<T> head;

        //public int Count { get; private set; }

        //public void AddFirst(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public void AddLast(T item)
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetFirst()
        //{
        //    throw new NotImplementedException();
        //}

        //public T GetLast()
        //{
        //    throw new NotImplementedException();
        //}

        //public T RemoveFirst()
        //{
        //    throw new NotImplementedException();
        //}

        //public T RemoveLast()
        //{
        //    throw new NotImplementedException();
        //}

        //public IEnumerator<T> GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    throw new NotImplementedException();
        //}
    }
}