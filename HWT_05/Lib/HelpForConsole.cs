namespace Lib
{
    using System;

    public class HelpForConsole //todo pn хорошо придумала, молодец
    {
        public static void PrintMenu(ClassName className)
        {
            Console.WriteLine("\nEnter the command:");
            Console.WriteLine("\t1: create a new default {0};", className);
            Console.WriteLine("\t2: create a new {0} with your parameters;", className);
            Console.WriteLine("\t0: close the program.");
        }

        public static double CheckAndSetParam()
        {
            var str = Console.ReadLine();
            double param;
            if (!double.TryParse(str, out param))
            {
                Console.WriteLine("The entered data is not a number. Enter again:");
                param = CheckAndSetParam();
            }

            return param;
        }

        public static Commands SetCommand()
        {
            var str = Console.ReadLine();
            Commands command;
            double com;
            if (!double.TryParse(str, out com))
            {
                Console.WriteLine("The entered data is not a command. Enter again:");
                command = SetCommand();
            }

            switch ((int)com)
            {
                case (int)Commands.createDefoult:
                case (int)Commands.createWithParam:
                case (int)Commands.exit:
                    {
                        command = (Commands)com;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("The entered data is not a command. Enter again:");
                        command = SetCommand();
                        break;
                    }
            }

            return command;
        }
    }
}
