namespace Task01
{
    using System.Linq;

    public static class ExtendedArray
    {
        /// <summary>
        /// Вычисляет сумму элементов массива
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int SumArray(this int[] array)
        {
            int sum = array.Sum();
            return sum;
        }
    }
}
