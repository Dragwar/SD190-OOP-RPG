using System;
using System.Collections;
using System.Collections.Generic;
using OOP_RPG.Models.Interfaces;

namespace OOP_RPG.Models
{
    public class ItemInventory<TItem> : IList<TItem>
        where TItem : IItem
    {
        private readonly IList<TItem> _underlyingList;

        public virtual event EventHandler<TItem> OnItemAdd;
        public virtual event EventHandler<TItem> OnItemRemove;

        public ItemInventory()
        {
            _underlyingList = new List<TItem>();
        }

        public ItemInventory(int initialCapacity)
        {
            _underlyingList = new List<TItem>(initialCapacity);
        }

        public ItemInventory(IEnumerable<TItem> initialItems)
        {
            _underlyingList = new List<TItem>(initialItems);
        }

        public virtual TItem this[int index]
        {
            get => _underlyingList[index];
            set => _underlyingList[index] = value;
        }

        public virtual int Count => _underlyingList.Count;

        public virtual bool IsReadOnly => _underlyingList.IsReadOnly;


        public virtual void Add(TItem item)
        {
            _underlyingList.Add(item);
            OnItemAdd?.Invoke(this, item);
        }

        public virtual void Insert(int index, TItem item)
        {
            _underlyingList.Insert(index, item);
            OnItemAdd?.Invoke(this, item);
        }

        public virtual bool Remove(TItem item)
        {
            bool result = _underlyingList.Remove(item);
            OnItemRemove?.Invoke(this, item);
            return result;
        }

        public virtual void RemoveAt(int index)
        {
            OnItemAdd?.Invoke(this, _underlyingList[index]);
            _underlyingList.RemoveAt(index);
        }

        public virtual void Clear()
        {
            if (OnItemRemove is { })
            {
                ForEach(item => OnItemRemove.Invoke(this, item));
            }

            _underlyingList.Clear();
        }

        public virtual bool Contains(TItem item) => _underlyingList.Contains(item);

        public virtual int IndexOf(TItem item) => _underlyingList.IndexOf(item);

        public virtual void CopyTo(TItem[] array, int arrayIndex) => _underlyingList.CopyTo(array, arrayIndex);

        public virtual void ForEach(Action<TItem> action)
        {
            foreach (var item in _underlyingList)
            {
                action.Invoke(item);
            }
        }

        public virtual void ForEach(Action<TItem, int> action)
        {
            for (var i = 0; i < _underlyingList.Count; i++)
            {
                var item = _underlyingList[i];
                action.Invoke(item, i);
            }
        }

        public virtual IEnumerable<T> OfType<T>()
            where T : TItem
        {
            for (int i = 0; i < Count; i++)
            {
                if (_underlyingList[i] is T item)
                {
                    yield return item;
                }
            }
        }

        public virtual IEnumerator<TItem> GetEnumerator() => _underlyingList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _underlyingList.GetEnumerator();
    }
}
