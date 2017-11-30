namespace Task01
{
    using System;

    public class Logic
    {
        public static double AreaCalc(double width, double height)
        {
            return width * height;
        }

        public static double SetPositiveValue(string name)
        {
            double value = SetValue(name);

            if (value <= 0)
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetPositiveValue(name);
            }

            return value;
        }

        public static double SetValue(string name)
        {
            Console.WriteLine("Enter {0}:", name);
            double value;

            if (!double.TryParse(Console.ReadLine(), out value))
            {
                Console.WriteLine("Incorrect data entered.");
                value = SetValue(name);
            }

            return value;
        }
    }
}
