using System.Linq;

namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Loader : IBuffer
    {
        private List<IEntity> _items;

        public Loader()
        {
            _items = new List<IEntity>();
        }

        public int EntitiesCount
        {
            get => this._items.Count;
        }

    public void Add(IEntity entity)
        {
            _items.Add(entity);
        }

        public void Clear()
        {
            this._items = new List<IEntity>();
        }

        public bool Contains(IEntity entity)
        {
           return _items.Contains(entity);
        }

        public IEntity Extract(int id)
        {
            IEntity searchedEntity = null;

            //searchedEntity = _items.FirstOrDefault(i => i.Id == id);

            //if (searchedEntity != null)
            //{
            //    _items.Remove(searchedEntity);
            //}

            return searchedEntity;
        }

        public IEntity Find(IEntity entity)
        {
            //IEntity searchedEntity = null;

            //if (this.isEmpty())
            //{
            //    return searchedEntity;
            //}

            //searchedEntity = _items.FirstOrDefault(i => i == entity);

            //return searchedEntity;

            return null;

        }

        public List<IEntity> GetAll()
        {
            return _items;
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                yield return this._items[i];
            }
        }

        public void RemoveSold()
        {
            //var items = _items.Where(e => e.Status.ToString() == "Sold").ToList();

            //foreach (var item in items)
            //{
            //    _items.Remove(item);
            //}
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            this.isValid(oldEntity);

            var index = FindIndex(oldEntity);

            _items[index] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var items = GetAll().Where(i => (int) i.Status >= (int) lowerBound && (int) i.Status <= (int) upperBound)
                .ToList();
            return items;
        }

        public void Swap(IEntity first, IEntity second)
        {

            var firstIndex = FindIndex(first);
            var secondIndex = FindIndex(second);


            var temp = first;

            _items[firstIndex] = second;
            _items[secondIndex] = temp;
        }

        public IEntity[] ToArray()
        {
            var entities = _items.ToArray();

            return entities;
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            var items = GetAll().Where(i => i.Status == oldStatus);

            foreach (var item in items)
            {
                item.Status = newStatus;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool isEmpty()
        {
            if (this.EntitiesCount < 1)
            {
                return true;
            }

            return false;
        }

        private void isValid(IEntity entity)
        {
            if (_items.Contains(entity))
            {
                throw new InvalidOperationException("Entity not found");
            }

        }

        private int FindIndex(IEntity entity)
        {
            for (int i = 0; i < this._items.Count; i++)
            {
                if (_items[i] == entity)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
