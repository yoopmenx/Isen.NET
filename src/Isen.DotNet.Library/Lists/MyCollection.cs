using System;
using System.Collections;
using System.Collections.Generic;

namespace Isen.DotNet.Library.Lists
{    
    public class MyCollection<T> : IList<T>
    {
        private T[] _values;

        public MyCollection()
        {
            _values = new T[0];
        }

        /// <summary>
        /// Taille de la liste
        /// </summary>
        public int Count => _values.Length;

        /// <summary>
        /// Accès aux valeurs de la liste
        /// </summary>
        protected T[] Values => _values;

        /// <summary>
        /// Accesseur indexeur
        /// </summary>
        /// <value></value>
        public T this[int index]
        {
            get { return _values[index]; }
            set { _values[index] = value; }
        }

        /// <summary>
        /// Ajoute un élément à la fin de la liste
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            // Nouveau tableau de taille + 1
            var tmp = new T[Count + 1];
            // Copier les éléments du tableau initial
            for (var i = 0 ; i < Count ; i++)
            {
                tmp[i] = _values[i];
            }
            // Ajouter le nouvel élément
            tmp[Count] = item;
            // Echanger les tableaux
            _values = tmp;
        }

        public void RemoveAt(int index)
        {
            if (Values?.Length == 0 
                || index > Count 
                || index < 0)
                throw new IndexOutOfRangeException();

            var tmp = new T[Count - 1];
            for (var i = 0 ; i < tmp.Length ; i++)
            {
                tmp[i] = _values[i < index ? i : i + 1];
            }
            _values = tmp;
        }

        public bool IsReadOnly => false;

        public int IndexOf(T item)
        {
            var index = -1;
            for (var i = 0 ; i < Count ; i++)
            {
                if (this[i].Equals(item))
                {
                    index = i;
                    break;
                }
            }
            return index;
        }

        public void Insert(int index, T item)
        {
            if (index > Count || index < 0)
                throw new ArgumentOutOfRangeException();

            // Nouveau tableau de taille + 1
            var tmp = new T[Count + 1];
            // Copier les éléments du tableau initial
            for (var i = 0 ; i < tmp.Length ; i++)
            {
                if (i < index) tmp[i] = _values[i];
                else if (i == index) tmp[i] = item;
                else tmp[i] = _values[i - 1];                
            }
            // Echanger les tableaux
            _values = tmp;
        }

        public void Clear()
        {
            _values = new T[0];
        }

        public bool Contains(T item) => 
            IndexOf(item) >= 0;

        public void CopyTo(
            T[] array, 
            int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException();
            if (arrayIndex < 0) throw new ArgumentOutOfRangeException();
            if (Count + arrayIndex > array.Length) throw new ArgumentException();
            
            for(var i = 0; i < Count ; i++)
            {
                array[arrayIndex + i] = this[i];
            }
        }

        public bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index < 0) return false;

            RemoveAt(index);
            return true;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0 ; i < Count ; i++)
            {
                yield return this[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() =>
            GetEnumerator();
    }
}