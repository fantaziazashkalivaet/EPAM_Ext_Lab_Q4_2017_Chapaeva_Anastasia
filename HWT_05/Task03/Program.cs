namespace Task03
{
    using System;
    using Lib;

    public class Program
    {
        public static void Main(string[] args)
        {
            bool exit = false;
            Commands command;

            while (!exit)
            {
                HelpForConsole.PrintMenu(ClassName.user);
                command = HelpForConsole.SetCommand();

                switch (command)
                {
                    case Commands.createDefoult:
                        {
                            var user = new User();
                            ForConsole.PrintUser(user);
                            break;
                        }

                    case Commands.createWithParam:
                        {
                            Console.Write("Enter the first name: ");
                            var firstName = Console.ReadLine();

                            Console.Write("\nEnter the second name: ");
                            var secondName = Console.ReadLine();

                            Console.Write("\nEnter the patronimic: ");
                            var patronimic = Console.ReadLine();

                            Console.Write("\nEnter the date of birth in format dd/MM/yyyy (exmpl: 01/01/1900): ");
                            var date = ForConsole.ReadAndCheckDate();

                            var user = new User(firstName, secondName, patronimic, date);
                            ForConsole.PrintUser(user);

                            break;
                        }

                    case Commands.exit:
                        {
                            exit = true;
                            break;
                        }
                }
            }
        }
    }
}
