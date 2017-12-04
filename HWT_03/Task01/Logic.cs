namespace Task01
{
    using System;
    using System.Linq;

    public class Logic
    {
        public static void PrintInfo()
        {
            Console.WriteLine("Enter the number (1-3) to working with this array or \"exit\" to complete or \"new\" to create new array:");
            Console.WriteLine("\t1: maximum of the array\n\t2: minimum of the array\n\t3: array sorting\n\t");
        }

        public static void PrintMaxOrMin(MyArray arr, string minOrMax, int? index)
        {
            if (index == null)
            {
                Console.WriteLine("Array is empty\n");
            }
            else
            {
                Console.WriteLine("{0} of the array: {1}\n", minOrMax, arr.Array[(int)index]);
            }
        }

        public static void PrintArray(MyArray arr)
        {
            if (arr.Array != null && arr.Array.Count() != 0)
            {
                string s = null;

                foreach (var e in arr.Array)
                {
                    s += e + " ";
                }

                Console.WriteLine("Array: " + s);
            }
            else
            {
                Console.WriteLine("Array is empty\n");
            }
        }

        public static bool ReadCommand(MyArray array, out bool newArray)
        {
            PrintInfo();
            string command = Console.ReadLine().ToLower();
            return CheckCommand(command, array, out newArray);
        }

        public static bool CheckCommand(string command, MyArray array, out bool newArray)
        {
            bool exit = false;
            newArray = false;

            switch (command)
            {
                case "1":
                    {
                        PrintMaxOrMin(array, "Maximum", array.GetMax());
                        break;
                    }

                case "2":
                    {
                        PrintMaxOrMin(array, "Minimum", array.GetMin());
                        break;
                    }

                case "3":
                    {
                        array.Sort();
                        PrintArray(array);
                        Console.WriteLine();
                        break;
                    }

                case "exit":
                    {
                        exit = true;
                        break;
                    }

                case "new":
                    {
                        newArray = true;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Incorrect data entered. Enter again:");
                        exit = ReadCommand(array, out newArray);
                        break;
                    }
            }

            return exit;
        }
    }
}
