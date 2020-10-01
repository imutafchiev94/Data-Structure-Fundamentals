using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace _03.PriorityQueue
{
    using System;

    public class PriorityQueue<T> : IAbstractHeap<T>
        where T : IComparable<T>
    {
        private List<T> _elements;

        public PriorityQueue()
        {
            this._elements = new List<T>();
        }

        public int Size => this._elements.Count;

        public T Dequeue()
        {
            var firstElement = this.Peek();
            this.Swap(0, this.Size - 1);
            this._elements.RemoveAt(this.Size - 1);
            this.HeapifyDown();

            return firstElement;
        }

        

        public void Add(T element)
        {
            this._elements.Add(element);

            this.HeapifyPop();
        }

        public T Peek()
        {
            this.EnsureNotEmpty();
            return this._elements[0];
        }

        private void HeapifyDown()
        {
            int parentIndex = 0;
            int leftChieldIndex = this.GetLeftChieldIndex(0);

            while (this.IndexIsValid(leftChieldIndex) &&
                   this.IsLess(parentIndex, leftChieldIndex))
            {
                int toSwapWith = leftChieldIndex;
                int rightChieldIndex = this.GetRightChieldIndex(0);

                if (this.IndexIsValid(rightChieldIndex) && 
                    IsLess(toSwapWith, rightChieldIndex))
                {
                    toSwapWith = rightChieldIndex;
                }

                Swap(toSwapWith, parentIndex);
                parentIndex = toSwapWith;
                leftChieldIndex = this.GetLeftChieldIndex(parentIndex);
            }
        }

        private bool IsGreater(int chieldIndex, int parentIndex)
        {
            return this._elements[chieldIndex].CompareTo(this._elements[parentIndex]) > 0;
        }

        private bool IsLess (int chieldIndex, int parentIndex)
        {
            return this._elements[chieldIndex].CompareTo(this._elements[parentIndex]) < 0;
        }

        private int GetParentIndex(int chieldIndex)
        {
            return (chieldIndex - 1) / 2;
        }

        private bool IndexIsValid(int index)
        {
            return true;
        }

        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Max heap is empty");
            }
        }

        private void Swap(int chieldindex, int parentIndex)
        {
            var temp = this._elements[parentIndex];
            this._elements[parentIndex] = this._elements[chieldindex];
            this._elements[chieldindex] = temp;
        }

        private void HeapifyPop()
        {
            int currentIndex = this.Size - 1;
            int chieldIndex = GetParentIndex(currentIndex);
            while (this.IndexIsValid(currentIndex) && IsGreater(currentIndex, chieldIndex))
            {
                var temp = this._elements[chieldIndex];
                this._elements[chieldIndex] = this._elements[currentIndex];
                this._elements[currentIndex] = temp;

                currentIndex = chieldIndex;
                chieldIndex = GetParentIndex(currentIndex);
            }
        }

        //private int GetGreaterChieldIndex(int currentIndex)
        //{
        //    var firstChieldIndex = 2 * currentIndex + 1;
        //    var secondChildIndex = 2 * currentIndex + 2;

        //    var compare = IsGreater(firstChieldIndex, secondChildIndex);

        //    if (compare)
        //    {
        //        return firstChieldIndex;
        //    }

        //    return secondChildIndex;
        //}

        private int GetLeftChieldIndex(int currentIndex)
        {
            return 2 * currentIndex + 1;
            
        }

        private int GetRightChieldIndex(int currentIndex)
        {
            return 2 * currentIndex + 2;

        }
    }
}
