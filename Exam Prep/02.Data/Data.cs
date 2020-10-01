using System.Linq;

namespace _02.Data
{
    using _02.Data.Interfaces;
    using System;
    using System.Collections.Generic;

    public class Data : IRepository
    {
        private List<IEntity> _entities;

        public Data()
        {
            _entities = new List<IEntity>();
        }

        public int Size => this._entities.Count;

        public void Add(IEntity entity)
        {
            this._entities.Add(entity);

            this.HeapifyPop();
        }

        private void HeapifyPop()
        {
            int currentIndex = this.Size - 1;
            int parentIndex = GetParentIndex(currentIndex);
            while (this.IndexIsValid(currentIndex) && IsGreater(currentIndex, parentIndex))
            {
                var temp = this._entities[parentIndex];
                this._entities[parentIndex] = this._entities[currentIndex];
                this._entities[currentIndex] = temp;

                currentIndex = parentIndex;
                parentIndex = GetParentIndex(currentIndex);
            }
        }

        private bool IndexIsValid(int index)
        {
            return index >= 0 && index < this.Size;
        }

        private int GetParentIndex(int currentIndex)
        {
            return (currentIndex - 1) / 2;
        }

        private bool IsGreater(int childIndex, int parentIndex)
        {
            return this._entities[childIndex].Id.CompareTo(this._entities[parentIndex].Id) > 0;
        }

        public IRepository Copy()
        {
            return new Data();
        }

        public IEntity DequeueMostRecent()
        {
            throw new NotImplementedException();
        }

        public List<IEntity> GetAll()
        {
            return _entities;
        }

        public List<IEntity> GetAllByType(string type)
        {
            return null;
        }

        public IEntity GetById(int id)
        {

            return _entities.FirstOrDefault(e => e.Id == id);
        }

        public List<IEntity> GetByParentId(int parentId)
        {
            return _entities.Where(e => e.ParentId == parentId).ToList();
        }

        public IEntity PeekMostRecent()
        {
            this.EnsureNotEmpty();

            return this._entities[0];
        }


        private void EnsureNotEmpty()
        {
            if (this.Size == 0)
            {
                throw new InvalidOperationException("Operation on empty Data");
            }
        }
    }
}
