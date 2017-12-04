namespace Task02
{
    using System;

    public class Helper
    {
        public static bool ReadCommand(int[,,] array, out bool isNewCreate)
        {
            Console.WriteLine("Enter \"exit\" to complete or \"new\" to create new array:");
            string command = Console.ReadLine().ToLower();
            return CheckCommand(array, command, out isNewCreate);
        }

        public static bool CheckCommand(int[,,] array, string command, out bool isNewCreate)
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

        public static int[,,] CreateArray()
        {
            var rnd = new Random();

            int sizeX = rnd.Next(2, 5);
            int sizeY = rnd.Next(2, 5);
            int sizeZ = rnd.Next(2, 5);
            var array = new int[sizeX, sizeY, sizeZ];

            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    for (var k = 0; k < sizeZ; k++)
                    {
                        array[i, j, k] = rnd.Next(-9, 9);
                    }
                }
            }

            return array;
        }
        
        public static void ChangePositiveNumbers(int[,,] array, int variableElem)
        {
            var rnd = new Random();

            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    for (var k = 0; k < array.GetLength(2); k++)
                    {
                        if (array[i, j, k] > 0)
                        {
                            array[i, j, k] = variableElem;
                        }
                    }
                }
            }
        }

        public static void PrintArray(int[,,] array)
        {
            for (var i = 0; i < array.GetLength(0); i++)
            {
                for (var j = 0; j < array.GetLength(1); j++)
                {
                    for (var k = 0; k < array.GetLength(2); k++)
                    {
                        Console.Write(array[i, j, k]);
                        Console.Write(' ');
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
            }
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

        public static int[,,] SetArray()
        {
            var rnd = new Random();

            int sizeX = rnd.Next(2, 5);
            int sizeY = rnd.Next(2, 5);
            int sizeZ = rnd.Next(2, 5);

            Console.WriteLine("Enter the elements of the array[{0}, {1}, {2}] through enter:", sizeX, sizeY, sizeZ);
            var array = new int[sizeX, sizeY, sizeZ];

            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    for (var k = 0; k < sizeZ; k++)
                    {
                        array[i, j, k] = CheckNumber(Console.ReadLine());
                    }
                }
            }

            return array;
        }
    }
}
