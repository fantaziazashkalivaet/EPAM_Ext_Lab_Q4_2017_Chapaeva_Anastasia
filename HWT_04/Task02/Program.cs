/*
 * Написать программу, которая удваивает в первой введенной строки все символы,
 * принадлежащие второй введенной строке.
 */

namespace Task02
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Logic.ReadAndCheckString(out exit);
            }
        }
    }
}
