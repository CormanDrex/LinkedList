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
            AddLast(value);
        }

        public void AddFirst(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);

            // Сохраняем ссылку на первый элемент.
            LinkedListNode<T> temp = _head;

            // _head указывает на новый узел.
            _head = node;

            // Вставляем список позади первого элемента.
            _head.Next = temp;

            if (Count == 0)
            {
                // Если список был пуст, то head and tail должны
                // указывать на новой узел.
                _tail = _head;
            }
            else
            {
                // До:    head -------> 5  7 -> null
                // После: head  -> 3  5  7 -> null
                temp.Previous = _head;
            }

            Count++;
        }

        public void AddLast(T value)
        {
            LinkedListNode<T> node = new LinkedListNode<T>(value);

            if (Count == 0)
            {
                _head = node;
            }
            else
            {
                _tail.Next = node;

                // До:    Head -> 3  5 -> null
                // После:Head -> 3  5  7 -> null
                // 7.Previous = 5
                node.Previous = _tail;
            }

            _tail = node;
            Count++;
        }

        public void RemoveFirst()
        {
            if (Count != 0)
            {
                // До:    Head -> 3  5
                // После: Head -------> 5

                // Head -> 3 -> null
                // Head ------> null
                _head = _head.Next;

                Count--;

                if (Count == 0)
                {
                    _tail = null;
                }
                else
                {
                    // 5.Previous было 3; теперь null.
                    _head.Previous = null;
                }
            }
        }

        public void RemoveLast()
        {
            if (Count != 0)
            {
                if (Count == 1)
                {
                    _head = null;
                    _tail = null;
                }
                else
                {
                    // До:    Head --> 3 --> 5 --> 7
                    //        Tail = 7
                    // После: Head --> 3 --> 5 --> null
                    //        Tail = 5
                    // Обнуляем 5.Next
                    _tail.Previous.Next = null;
                    _tail = _tail.Previous;
                }

                Count--;
            }
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
            LinkedListNode<T> current = _head;
            while (current != null)
            {
                array[arrayIndex++] = current.Value;
                current = current.Next;
            }
        }

        public int Count
        {
            get;
            private set;
        }

        public bool IsReadOnly
        {
            get { return false; }
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
                // Head -> 3 -> 5 -> 7 -> null
                // Head -> 3 ------> 7 -> null
                if (current.Value.Equals(item))
                {
                    // Узел в середине или в конце.
                    if (previous != null)
                    {
                        // Случай 3b.
                        previous.Next = current.Next;

                        // Если в конце, то меняем _tail.
                        if (current.Next == null)
                        {
                            _tail = previous;
                        }
                        else
                        {
                            // До:    Head -> 3  5  7 -> null
                            // После: Head -> 3  7 -> null

                            // previous = 3
                            // current = 5
                            // current.Next = 7
                            // Значит... 7.Previous = 3
                            current.Next.Previous = previous;
                        }

                        Count--;
                    }
                    else
                    {
                        // Случай 2 или 3a.
                        RemoveFirst();
                    }

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

        public void PrintList()
        {
            LinkedListNode<T> current = _head;
            int i = 0;

            Console.Write("║Node Data\t║");
            while (current != null)
            {
                Console.Write(current.Value + "\t║");
                current = current.Next;
            }

            Console.Write("\n║Node Address\t║");
            while (i != Count) {
                Console.Write(i + "\t║");
                i++;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Создаём новый связный список и инициализируем переменные
            LinkedList<int> list = new LinkedList<int>();
            int i = 0;
            Random rand = new Random(); // генератор случайных чисел

            // Заполняем список случайными числами
            while (i <= 10)
            {
                list.Add(rand.Next(0,100));
                i++;
            }

            list.PrintList();
            Console.ReadKey();
        }
    }
}
