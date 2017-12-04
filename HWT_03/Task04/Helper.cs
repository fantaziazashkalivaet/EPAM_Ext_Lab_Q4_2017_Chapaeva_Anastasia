namespace Task04
{
    using System;

    public class Helper
    {
        public static int? SumPos(int[,] array)
        {
            int sum = 0;
            for (var i = 0; i < array.GetLength(0); i++)
            {
                int stepJ = 1;
                int startJ = 1;
                if (i % 2 == 0)
                {
                    startJ = 0;
                    stepJ = 2;
                }

                for (var j = startJ; j < array.GetLength(1); j += stepJ)
                {
                    if ((i + j) % 2 == 0)
                    {
                        try
                        {
                            sum = checked(sum + array[i, j]);
                        }
                        catch
                        {
                            Console.WriteLine("Sum of non-negative elements of the array: no end of!");
                        }
                    }
                }
            }

            return sum;
        }

        public static int[,] CreateArray()
        {
            var rnd = new Random();

            int sizeX = rnd.Next(2, 5);
            int sizeY = rnd.Next(2, 5);
            var array = new int[sizeX, sizeY];

            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    array[i, j] = rnd.Next(-9, 9);
                }
            }

            return array;
        }

        public static void PrintArray(int[,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j]);
                    Console.Write(' ');
                }

                Console.WriteLine();
            }
        }

        public static bool ReadCommand(int[,] array, out bool isNewCreate)
        {
            Console.WriteLine("Enter \"exit\" to complete or \"new\" to create new array:");
            string command = Console.ReadLine().ToLower();
            return CheckCommand(array, command, out isNewCreate);
        }

        public static bool CheckCommand(int[,] array, string command, out bool isNewCreate)
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
    }
}
