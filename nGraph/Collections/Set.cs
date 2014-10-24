/*
Copyright 2012 by Timothy Schruben.
All rights reserved.
See LICENSE.txt for permissions.
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Collections;
using System.Linq;

namespace nGraph.Collections
{
    public class Set<T> : ICollection<T>
    {
        private readonly ISet<T> _set;

        public Set()
        {
            _set = new HashSet<T>();
        }

        public Set(IEqualityComparer<T> comparer)
        {
            _set = new HashSet<T>(comparer);
        }

        public Set(IEnumerable<T> enumerable)
            : this()
        {
            AddRange(enumerable);
        }

        #region ICollection<T> Members

        public virtual void Add(T item)
        {
            if (_set.Contains(item))
                return;

            _set.Add(item);
        }

        public virtual void AddRange(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException("enumerable");

            enumerable.AsParallel().ForAll(Add);
        }

        public virtual void Clear()
        {
            _set.Clear();
        }

        public virtual bool Contains(T item)
        {
            return _set.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            _set.CopyTo(array, arrayIndex);
        }

        public virtual int Count
        {
            get { return _set.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return _set.IsReadOnly; }
        }

        public virtual bool Remove(T item)
        {
            return _set.Remove(item);
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _set.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// Gets intersection performing a minimal number of comparisons and by using the hashbucket(s) of the dictionaries.
        /// </summary>
        /// <param name="sets">Sets to find the intersection of this set with.</param>
        /// <returns>The intersection. If <typeparamref name="T"/> is a reference type, the reference will be from this instance.</returns>
        public Set<T> GetIntersection(params Set<T>[] sets)
        {
            if (sets == null) throw new ArgumentNullException("sets");
            if (sets.Length == 0) throw new ArgumentException("Array length must be greater than zero.", "sets");

            // We want to make sure that the outer loop is counting the set with the least number of members
            //
            List<Set<T>> sortedList = GetSortedListForIntersection(sets);
            Set<T> intersection = new Set<T>();
            Set<T> alpha = sortedList[0];
            sortedList.RemoveAt(0);

            foreach (T t in alpha)
            {
                bool keepItem = true;
                foreach (Set<T> set in sortedList)
                {
                    if (!set._set.ContainsKey(t))
                    {
                        keepItem = false;
                        break;
                    }
                }
                if (keepItem)
                {
                    intersection.Add(_set[t]); // For reference types, we will return members of this set that are 'equal'.
                }
            }

            return intersection;

        }

        /// <summary>
        /// Adds this to the list, and sorts so that sets with a lesser count get a lower sort order.
        /// </summary>
        /// <param name="sets"></param>
        /// <returns></returns>
        private List<Set<T>> GetSortedListForIntersection(params Set<T>[] sets)
        {
            List<Set<T>> intersectionList = new List<Set<T>>(sets);
            intersectionList.Add(this);
            intersectionList.Sort(new SetCountComparer<T>());
            return intersectionList;
        }

        private List<Set<T>> GetSets(params IEnumerable<T>[] enumerables)
        {
            List<Set<T>> sets = new List<Set<T>>(enumerables.Length);
            foreach (IEnumerable<T> enumerable in enumerables)
            {
                if (enumerable.GetType() == typeof(Set<T>))
                {
                    sets.Add((Set<T>)enumerable);
                }
                sets.Add(new Set<T>(enumerable));
            }
            return sets;
        }

        /// <summary>
        /// A flexible but less performant method that allows comparison with any enumerable type(s).
        /// </summary>
        /// <param name="enumerables">Instances of <see cref="IEnumerable&lt;T&gt;"/>.</param>
        /// <returns>The intersection. If the type is a reference type, the members will be taken from this set.</returns>
        public Set<T> GetIntersection(params IEnumerable<T>[] enumerables)
        {
            if (enumerables == null) throw new ArgumentNullException("enumerables");
            if (enumerables.Length == 0) throw new ArgumentException("Array length must be greater than zero.", "enumerables");

            return GetIntersection(GetSortedListForIntersection(GetSets(enumerables).ToArray()).ToArray());
        }

        /// <summary>
        /// Returns a <see cref="ReadOnlyCollection&lt;T&gt;"/> created from the members of this instance.
        /// </summary>
        /// <returns>A <see cref="ReadOnlyCollection&lt;T&gt;"/> containing the members of this instance.</returns>
        public ReadOnlyCollection<T> ToReadOnly()
        {
            return new ReadOnlyCollection<T>(new List<T>(_set.Keys));
        }
    }
}
