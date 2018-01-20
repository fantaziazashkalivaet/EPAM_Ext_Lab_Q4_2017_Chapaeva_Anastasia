namespace Task01
{
    public class Joseph
    {
        /// <summary>
        /// Рекурсивное решение задачи Иосифа
        /// </summary>
        /// <param name="n">Количество человек в кругу</param>
        /// <param name="k">Каждое k-е число удаляется</param>
        /// <returns>Последнее число, оставшееся в кругу</returns>
        public static int Solution(int n, int k)
        {
            return n > 1 ? (((Solution(n - 1, k) + k - 1) % n) + 1) : 1;
        }
    }
}
