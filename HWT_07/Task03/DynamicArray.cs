namespace Task03
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class DynamicArray<T> : IEnumerable<T> where T : IComparable<T>
    {
        private const int InitialCapacity = 8;
        private const int CapacitanceMultiplier = 2;
        private T[] array;
        private int size;

        /// <summary>
        /// Инициализирует новый экземпляр класса DynamicArray, который является пустым и имеет начальную емкость по умолчанию.
        /// </summary>
        public DynamicArray() : this(InitialCapacity)
        {
        }

        /// <summary>
        /// Инициализирует новый пустой экземпляр класса DynamicArray с указанной начальной емкостью.
        /// </summary>
        /// <param name="capacity">Начальная емкость</param>
        public DynamicArray(int capacity)
        {
            if (capacity > 0)
            {
                array = new T[capacity];
            }
            else
            {
                array = new T[InitialCapacity];
            }

            size = capacity;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса DynamicArray, который содержит элементы, скопированные из указанной коллекции, и обладает начальной емкостью, равной количеству скопированных элементов.
        /// </summary>
        /// <param name="collection">Интерфейс IEnumerable<T>, элементы которого копируются в новый список</param>
        public DynamicArray(IEnumerable<T> collection)
        {
            array = new T[collection.Count()];
            this.AddRange(collection);
        }

        /// <summary>
        /// Возвращает число элементов, содержащихся в DynamicArray
        /// </summary>
        public int Length
        {
            get
            {
                return size;
            }
        }

        /// <summary>
        /// Возвращает или задает число элементов, которое может содержать список DynamicArray
        /// </summary>
        public int Capacity
        {
            get
            {
                return array.Count();
            }
        }

        /// <summary>
        /// Возвращает объект T из текущей коллекции DynamicArray, содержащийся в указанной позиции  
        /// </summary>
        /// <param name="i">Позиция объекта</param>
        /// <returns>Объект коллекции DynamicArray в позиции i</returns>
        public T this[int i]
        {
            get
            {
                if (i < size)
                {
                    return array[i];
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }

            set
            {
                if (i < size && i >= 0)
                {
                    array[i] = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Добавляет объект в конец очереди DynamicArray
        /// </summary>
        /// <param name="value">Добавляемый объект</param>
        public void Add(T value)
        {
            if (size == Capacity)
            {
                this.ExpandCapacity();
            }

            array[size] = value;
            size++;
        }

        /// <summary>
        /// Добавляет элементы интерфейса IEnumerable<T> в конец списка DynamicArray
        /// </summary>
        /// <param name="collection"></param>
        public void AddRange(IEnumerable<T> collection)
        {
            if (Capacity < size + collection.Count())
            {
                int multiplier = 0;
                int newCapacity = Capacity;
                while (newCapacity < size + collection.Count())
                {
                    newCapacity *= CapacitanceMultiplier;
                    multiplier++;
                }

                ExpandCapacity(multiplier);
            }

            foreach (var value in collection)
            {
                array[size] = value;
            }

            size += collection.Count();
        }

        /// <summary>
        /// Удаляет первое вхождение указанного объекта из коллекции DynamicArray
        /// </summary>
        /// <param name="value">Удаляемый объект</param>
        /// <returns>true - объект успешно удален, false - объект не удалось удалить</returns>
        public bool Remove(T value)
        {
            int indexValue = Array.IndexOf(array, value);

            if (indexValue >= 0 && indexValue < size - 1)
            {
                for (var i = indexValue; i < size - 1; i++)
                {
                    array[i] = array[i + 1];
                }

                size--;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Вставляет элемент в коллекцию DynamicArray по указанному индексу.
        /// </summary>
        /// <param name="insertIndex">Отсчитываемый от нуля индекс, по которому следует вставить элемент value</param>
        /// <param name="value">Вставляемый объект T</param>
        /// <returns>true - успешная вставка, false - не удалось вставить объект</returns>
        public bool Insert(int insertIndex, T value)
        {
            if (insertIndex < 0 || insertIndex >= size)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (size == Capacity)
            {
                ExpandCapacity();
            }

            size++;

            for (var i = size; i > insertIndex; i--)
            {
                array[i] = array[i - 1];
            }

            array[insertIndex] = value;

            return true;
        }

        /// <summary>
        /// Возвращает объект IEnumerator для DynamicArray
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            for (var i = 0; i < size; i++)
            {
                yield return array[i];
            }
        }

        /// <summary>
        /// Возвращает перечислитель коллекции DynamicArray
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Увеличивает емкость коллекции в multiplier раз
        /// </summary>
        private void ExpandCapacity(int multiplier = CapacitanceMultiplier)
        {
            var tmp = new T[this.Capacity * multiplier];
            array.CopyTo(tmp, 0);
            array = tmp;
        }
    }
}
