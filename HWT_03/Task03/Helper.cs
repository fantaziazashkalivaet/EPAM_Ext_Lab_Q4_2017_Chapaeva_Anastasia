namespace Task03
{
    using System;

    public class Helper
    {
        public static int? SumNotNeg(int[] array)
        {
            int sum = 0;
            foreach (var e in array)
            {
                if (e > 0)
                {
                    try
                    {
                        sum = checked(sum + e);
                    }
                    catch
                    {
                        Console.WriteLine("Sum of non-negative elements of the array: no end of!");
                        return null;
                    }
                }
            }

            return sum;
        }

        public static void PrintArray(int[] array)
        {
            Console.Write("Array: ");
            for (var i = 0; i < array.GetLength(0); i++)
            { 
                Console.Write(array[i]);
                Console.Write(' ');
            }

            Console.Write('\n');
        }

        public static bool ReadCommand(int[] array, out bool isNewCreate)
        {
            Console.WriteLine("Enter \"exit\" to complete or \"new\" to create new array:");
            string command = Console.ReadLine().ToLower();
            return CheckCommand(array, command, out isNewCreate);
        }

        public static bool CheckCommand(int[] array, string command, out bool isNewCreate)
        {
            bool exit = false;
            isNewCreate = false;

            switch (command)
            {
                case "new":
                    {
                        Console.Clear();
                        isNewCreate = true;
                        break;
                    }

                case "exit":
                    {
                        exit = true;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Incorrect data entered. Enter again:");
                        exit = ReadCommand(array, out isNewCreate);
                        break;
                    }
            }

            return exit;
        }

        public static int CheckNumber(string number)
        {
            int num;
            
            try
            {
                num = int.Parse(number);
            }
            catch
            {
                Console.WriteLine("Incorrect number. Enter again:");
                number = Console.ReadLine();
                num = CheckNumber(number);
            }

            return num;
        }

        public static int[] SetArray()
        {
            var rnd = new Random();

            int sizeX = rnd.Next(5, 10);
            var array = new int[sizeX];
            Console.WriteLine("Enter the elements of the array[{0}] through enter:", sizeX);

            for (var i = 0; i < sizeX; i++)
            {
                array[i] = CheckNumber(Console.ReadLine());
            }

            return array;
        }
    }
}
