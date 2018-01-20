namespace Task01
{
    public class CompareString
    {
        public delegate bool Сomparison(string first, string second);

        /// <summary>
        /// Сравнивает строку first со строкой second
        /// </summary>
        /// <param name="first">первая строка</param>
        /// <param name="second">вторая строка</param>
        /// <returns>Возвращает true, если first меньше second, иначе false</returns>
        public static bool Compare(string first, string second)
        {
            if (first.Length != second.Length)
            {
                return first.Length < second.Length;
            }
            else
            {
                for (var i = 0; i < first.Length; i++)
                {
                    if (first[i] != second[i])
                    {
                        return first[i] < second[i];
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Сортирует массив строк с помощью указанного метода compare
        /// </summary>
        /// <param name="strings">Исходный массив строк</param>
        /// <param name="compare">Метод, выполняющий сравнение строк</param>
        public static void Sort(string[] strings, Сomparison compare)
        {
            MergeSort(strings, 0, strings.Length - 1, compare); 
        }

        /// <summary>
        /// Выполняет сортировку слиянием
        /// </summary>
        /// <param name="strings">Исходный массив строк</param>
        /// <param name="left">Левый индекс сортируемого массива</param>
        /// <param name="right">Правый индекс сортируемого массива</param>
        /// <param name="compare">Метод, выполняющий сравнение строк</param>
        private static void MergeSort(string[] strings, int left, int right, Сomparison compare)
        {
            if (left < right)
            {
                int middle = (right + left) / 2;

                MergeSort(strings, left, middle, compare);
                MergeSort(strings, middle + 1, right, compare);

                Merge(strings, left, middle, right, compare);
            }
        }

        private static void Merge(string[] strings, int left, int middle, int right, Сomparison compare)
        {
            int positionLeft = left;
            int positionRight = middle + 1;
            int sizeTmp = right - left + 1;
            var tmp = new string[sizeTmp];
            int positionTmp = 0;

            while (positionLeft <= middle && positionRight <= right)
            {
                if (compare(strings[positionLeft], strings[positionRight]))
                {
                    tmp[positionTmp] = strings[positionLeft];
                    positionLeft++;
                    positionTmp++;
                }
                else
                {
                    tmp[positionTmp] = strings[positionRight];
                    positionRight++;
                    positionTmp++;
                }
            }

            while (positionLeft <= middle)
            {
                tmp[positionTmp] = strings[positionLeft];
                positionLeft++;
                positionTmp++;
            }

            while (positionRight <= right)
            {
                tmp[positionTmp] = strings[positionRight];
                positionRight++;
                positionTmp++;
            }

            for (var i = 0; i < sizeTmp; i++)
            {
                strings[left + i] = tmp[i];
            }
        }
    }
}
