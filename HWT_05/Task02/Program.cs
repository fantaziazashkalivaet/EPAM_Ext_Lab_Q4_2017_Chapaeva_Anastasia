/* 
 * Написать класс, описывающий треугольник со сторонами a, b, c, и
 * позволяющий осуществить расчёт его площади и периметра. Написать
 * программу, демонстрирующую использование данного треугольника.
 */

namespace Task02
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
                HelpForConsole.PrintMenu(ClassName.triangle);
                command = HelpForConsole.SetCommand();

                switch (command)
                {
                    case Commands.createDefoult:
                        {
                            var triangle = new Triangle();
                            ForConsole.PrintTriangle(triangle);
                            break;
                        }

                    case Commands.createWithParam:
                        {
                            Console.Write("Enter the side a: ");
                            double a = HelpForConsole.CheckAndSetParam();
                            Console.Write("\nEnter the side b: ");
                            double b = HelpForConsole.CheckAndSetParam();
                            Console.Write("\nEnter the side c: ");
                            double c = HelpForConsole.CheckAndSetParam();
                            var triangle = new Triangle(a, b, c);
                            ForConsole.PrintTriangle(triangle);
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
