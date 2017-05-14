using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApplication1
{
    public class LinkedListNode<T>
    {
        ///
        /// Конструктор нового узла со значением Value.
        ///
        ///
        public LinkedListNode(T value)
        {
            Value = value;
        }

        ///
        /// Поле Value.
        ///
        public T Value { get; internal set; }

        ///
        /// Ссылка на следующий узел списка (если узел последний, то null).
        ///
        public LinkedListNode<T> Next { get; internal set; }

        ///
        /// Ссылка на предыдущий узел списка (если узел первый, то null).
        ///
        public LinkedListNode<T> Previous { get; internal set; }
    }

    public class LinkedList<T> :
    System.Collections.Generic.ICollection<T>
    {
        private LinkedListNode<T> _head;
        private LinkedListNode<T> _tail;

        public void Add(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);

            if (_head == null)
            {
                _head = node;
                _tail = node;
            }
            else
            {
                _tail.Next = node;
                _tail = node;
            }

            Count++;
        }

        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        public bool Contains(T item)
        {
            LinkedListNode<T> current = _head;
            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    return true;
                }

                current = current.Next;
            }

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly
        {
            get { throw new System.NotImplementedException(); }
        }

        public bool Remove(T item)
        {
            LinkedListNode<T> previous = null;
            LinkedListNode<T> current = _head;

            // 1: Пустой список: ничего не делать.
            // 2: Один элемент: установить Previous = null.
            // 3: Несколько элементов:
            //    a: Удаляемый элемент первый.
            //    b: Удаляемый элемент в середине или конце.

            while (current != null)
            {
                if (current.Value.Equals(item))
                {
                    // Узел в середине или в конце.
                    if (previous != null)
                    {
                        // Случай 3b.

                        // До:    Head -> 3 -> 5 -> null
                        // После: Head -> 3 ------> null
                        previous.Next = current.Next;

                        // Если в конце, то меняем _tail.
                        if (current.Next == null)
                        {
                            _tail = previous;
                        }
                    }
                    else
                    {
                        // Случай 2 или 3a.

                        // До:    Head -> 3 -> 5
                        // После: Head ------> 5

                        // Head -> 3 -> null
                        // Head ------> null
                        _head = _head.Next;

                        // Список теперь пустой?
                        if (_head == null)
                        {
                            _tail = null;
                        }
                    }

                    Count--;

                    return true;
                }

                previous = current;
                current = current.Next;
            }

            return false;
        }
       
        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            LinkedListNode<T> current = _head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }

        System.Collections.IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
