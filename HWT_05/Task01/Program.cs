/* Написать класс Round, задающий круг с указанными координатами
 * центра, радиусом, а также свойствами, позволяющими узнать длину
 * описанной окружности и площадь круга.Обеспечить нахождение
 * объекта в заведомо корректном состоянии.Написать программу,
 * демонстрирующую использование данного круга.
 */

namespace Task01
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
                HelpForConsole.PrintMenu(ClassName.round);
                command = HelpForConsole.SetCommand();

                switch (command)
                {
                    case Commands.createDefoult:
                        {
                            var round = new Round();
                            ForConsole.PrintRound(round);
                            break;
                        }

                    case Commands.createWithParam:
                        {
                            Console.Write("Enter the center coordinate along the X axis: ");
                            double x = HelpForConsole.CheckAndSetParam();
                            Console.Write("\nEnter the center coordinate along the Y axis: ");
                            double y = HelpForConsole.CheckAndSetParam();
                            Console.Write("\nEnter the radius: ");
                            double radius = HelpForConsole.CheckAndSetParam();
                            var round = new Round(x, y, radius);
                            ForConsole.PrintRound(round);
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
