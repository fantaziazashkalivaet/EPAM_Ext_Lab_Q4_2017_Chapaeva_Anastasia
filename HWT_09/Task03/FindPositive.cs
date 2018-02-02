namespace Task03
{
    using System.Collections.Generic;
    using System.Linq;

    public static class FindPositive
    {
        public delegate bool Compare(int num);

        /// <summary>
        /// Метод, реализующий поиск положительных чисел напрямую
        /// </summary>
        public static List<int> FindPositiveSimple(List<int> arr)
        {
            var countList = new List<int>();
            foreach (var e in arr)
            {
                if (IsPositive(e))
                {
                    countList.Add(e);
                }
            }

            return countList;
        }

        /// <summary>
        /// Метод, которому передается условие поиска положительных чисел через делегат
        /// </summary>
        public static List<int> FindPositiveDelegate(List<int> arr, Compare comp)
        {
            if (arr.Count == 0)
            {
                return null;
            }

            var countList = new List<int>();
            foreach (var e in arr)
            {
                if (comp(e))
                {
                    countList.Add(e);
                }
            }

            return countList;
        }

        /// <summary>исе
        /// Метод, которому передается условие поиска положительных чисел через делегат в виде анонимного метода
        /// </summary>
        public static List<int> FindPositiveAnonDelegate(List<int> arr)
        {
            return FindPositiveDelegate(arr, delegate (int x) { return IsPositive(x); });
        }

        /// <summary>
        /// Метод, которому передается условие поиска положительных чисел через делегат в виде лямбда-выражения
        /// </summary>
        public static List<int> FindPositiveLambdaDelegate(List<int> arr)
        {
            return FindPositiveDelegate(arr, x => IsPositive(x));
        }

        /// <summary>
        /// Метод, которому передается условие поиска положительных чисел через делегат в виде linq-выражения
        /// </summary>
        public static List<int> FindPositiveLinqDelegate(List<int> arr)
        {
            var count = from e in arr
                        where IsPositive(e)
                        select e;

            return count.ToList();
        }

        public static bool IsPositive(int num)
        {
            return num > 0;
        }
    }
}
