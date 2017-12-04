namespace Task01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            MyArray arr = new MyArray();
            bool exit = false;
            bool newArray = false;

            while (!exit)
            {
                if (newArray)
                {
                    arr = new MyArray();
                }

                Logic.PrintArray(arr);
                exit = Logic.ReadCommand(arr, out newArray);
            }
        }
    }
}
