namespace Task04
{
    using System;
    using System.Text;

    public class Logic
    {
        public static void PrintFir(int n)
        {
            for (var i = n - 1; i >= 0; i--)
            {
                PrintTriangle(n - i, n - 1);
            }
        }

        public static void PrintTriangle(int n, int sizeSpace)
        {
            var stars = new StringBuilder();
            var space = new StringBuilder();
            space.Append(' ', sizeSpace);

            for (var i = 0; i < n; i++)
            {
                Console.WriteLine(space.ToString() + stars.ToString() + stars.Append('*').ToString());

                if (i != n - 1)
                {
                    space.Length = space.Length - 1;
                }
            }
        }

        public static int SetPositiveValue(string name)
        {
            int value = SetValue(name);

            if (value <= 0)
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetPositiveValue(name);
            }

            return value;
        }

        public static int SetValue(string name)
        {
            Console.WriteLine("Enter {0}:", name);
            double value;

            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetValue(name);
            }

            return (int)value;
        }
    }
}
