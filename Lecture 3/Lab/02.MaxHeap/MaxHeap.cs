using System.Collections.Generic;

namespace _02.MaxHeap
{
    using System;

    public class MaxHeap<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public MaxHeap()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyPop();
        }

        private void HeapifyPop()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = GetParentIndex(currentIndex);
            while (this.IndexIsValid(currentIndex) && isGreater(currentIndex, parentIndex))
            {
                var temp = this._elements[parentIndex];
                this._elements[parentIndex] = this._elements[currentIndex];
                this._elements[currentIndex] = temp;

                currentIndex = parentIndex;
                parentIndex = GetParentIndex(currentIndex);
            }
        }

        private bool isGreater(int chieldIndex, int parentIndex)
        {
            return this._elements[chieldIndex].CompareTo(this._elements[parentIndex]) > 0;
        }

        private int GetParentIndex(int chieldIndex)
        {
            return (chieldIndex - 1) / 2;
        }

        private bool IndexIsValid(int index)
        {
            return index >= 0 && index < this.Size;
        }

        public T Peek()
        {

            this.EnsureNotEmpty();

            return this._elements[0];
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Max heap is empty");
            }
        }
    }
}
