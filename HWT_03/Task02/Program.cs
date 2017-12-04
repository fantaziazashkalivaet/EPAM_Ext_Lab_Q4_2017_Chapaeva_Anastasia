namespace Task02
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;
            var array = Helper.CreateArray();
            bool isNewCreate = true;

            while (!exit)
            {
                if (isNewCreate)
                {
                    array = Helper.SetArray();
                }

                Console.WriteLine("Old array:");
                Helper.PrintArray(array);
                Console.WriteLine();
                Console.WriteLine("New array:");
                Helper.ChangePositiveNumbers(array, 0);
                Helper.PrintArray(array);

                exit = Helper.ReadCommand(array, out isNewCreate);
            }   
        }
    }
}
