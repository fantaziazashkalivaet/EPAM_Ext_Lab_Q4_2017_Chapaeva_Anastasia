namespace Task05
{
    public class Logic
    {
        /// <summary>
        /// Вычисление суммы чисел, кратных двум введенным значениям
        /// </summary>
        /// <param name="n">Максимальное значение</param>
        /// <param name="dividFirst">Первый делитель</param>
        /// <param name="dividSecond">Второй делитель</param>
        /// <returns></returns>
        public static int Calc(int n, int dividFirst, int dividSecond)
        {
            int count = SumDiv(dividFirst, n);
            count += SumDiv(dividSecond, n);
            count -= SumDiv(dividFirst * dividSecond, n);

            return count;
        }

        /// <summary>
        /// Вычисление суммы чисел, кратным введенному значению
        /// </summary>
        /// <param name="div">Делитель</param>
        /// <param name="end">Максимальное число</param>
        /// <returns></returns>
        public static int SumDiv(int div, int end)
        {
            int sum = 0;
            for (var i = div; i < end; i += div)
            {
                sum += i;
            }

            return sum;
        }
    }
}
