namespace Task03
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                ForConsole.PrintMenu();
                exit = ForConsole.CheckCommand();
            }
        }
    }
}
