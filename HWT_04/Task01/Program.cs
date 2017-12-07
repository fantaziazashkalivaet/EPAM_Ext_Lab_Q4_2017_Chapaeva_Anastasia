/*
 * Написать программу, которая определяет среднюю длину слова во введенной текстовой строке.
 * Учесть, что символы пунктуации на длину слов влиять не должны.
 * Регулярные выражения не использовать. И не пытайтесь прописать все ручками.
 * Используйте стандартные методы класса String.
 */

namespace Task01
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
