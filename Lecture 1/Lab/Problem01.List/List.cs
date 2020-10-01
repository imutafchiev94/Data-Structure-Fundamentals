namespace Problem01.List
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class List<T> : IAbstractList<T>
    {
        private const int DEFAULT_CAPACITY = 4;
        private T[] _items;

        public List()
            : this(DEFAULT_CAPACITY) {
        }

        public List(int capacity)
        {
            if (capacity <= 0)
            {
                throw new IndexOutOfRangeException($"{capacity} is not valid capacity!");
            }

            this._items = new T[capacity];
        }


        public T this[int index]
        {
            get
            {
                CheckIndex(index);
                return this._items[index];
            }
            set
            {
                CheckIndex(index);
                this._items[index] = value;
            }
        }

        public int Count
        {
            get;
            private set;
        }

        public void Add(T item)
        {
            this.EnsureNotEmpty();
            
            this._items[this.Count++] = item;

        }

        

        public bool Contains(T item)
        {
            bool isContains = false;

            foreach (var seacherdItem in _items)
            {
                if (seacherdItem.Equals(item))
                {
                    isContains = true;
                }
            }

            return isContains;
        }


        public int IndexOf(T item)
        {
            int index = -1;

            for (int i = 0; i < this._items.Length; i++)
            {
                if (_items[i].Equals(item))
                {
                    index = i;
                    break;
                }

            }

            return index;
        }

        public void Insert(int index, T item)
        {
                CheckIndex(index);

                EnsureNotEmpty();
                
                for (int i = this.Count; i > index; i--)
                {
                    this._items[i] = this._items[i - 1];
                }

                this._items[index] = item;
                this.Count++;
            
            
        }

        

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveAt(int index)
        {
            CheckIndex(index);
            for (int i = index; i < this.Count - 1; i++)
            {
                this._items[i] = _items[i + 1];
            }

            this._items[this.Count - 1] = default;
            this.Count--;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
            {
                yield return this._items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => this.GetEnumerator();


        private void EnsureNotEmpty()
        {
            if (this.Count == this._items.Length)
            {
                this.Resize();
            }
        }

        private void Resize()
        {
            var newArray = new T[this._items.Length * 2];
            for (int i = 0; i < this._items.Length; i++)
            {
                newArray[i] = this._items[i];
            }

            this._items = newArray;
        }

        private void CheckIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException($"{index} is not bounds of the List");
            }

        }
    }
}