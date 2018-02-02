using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
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
