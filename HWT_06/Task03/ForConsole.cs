namespace Task03
{
    using System;

    public class ForConsole
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Enter a number to add a figure or end:");
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Line, FigureType.Line);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Rectangle, FigureType.Rectangle);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Circle, FigureType.Circle);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Round, FigureType.Round);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Ring, FigureType.Ring);
            Console.WriteLine("\t0: end.");
        }

        public static bool CheckCommand(ref Figures figures)
        {
            Random r = new Random();
            var command = SetCommand();
            bool exit = false;

            switch (command)
            {
                case (int)FigureType.Line:
                    {
                        var figure = new Line(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        figures.SetFigures(figure);
                        Console.WriteLine(figure.Info());
                        break;
                    }

                case (int)FigureType.Rectangle:
                    {
                        var figure = new Rectangle(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        figures.SetFigures(figure);
                        Console.WriteLine(figure.Info());
                        break;
                    }

                case (int)FigureType.Circle:
                    {
                        var figure = new Circle(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        figures.SetFigures(figure);
                        Console.WriteLine(figure.Info());
                        break;
                    }

                case (int)FigureType.Round:
                    {
                        var figure = new Round(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        figures.SetFigures(figure);
                        Console.WriteLine(figure.Info());
                        break;
                    }

                case (int)FigureType.Ring:
                    {
                        var figure = new Ring(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        figures.SetFigures(figure);
                        Console.WriteLine(figure.Info());
                        break;
                    }

                case 0:
                    {
                        exit = true;
                        break;
                    }

                default:
                    {
                        Console.WriteLine("Command does not exist.");
                        exit = CheckCommand(ref figures);
                        break;
                    }
            }

            return exit;
        }

        public static int SetCommand()
        {
            var str = Console.ReadLine();
            int command;
             if (!int.TryParse(str, out command))
                {
                    Console.WriteLine("Incorrect command entered. Enter again");
                    SetCommand();
                }

            return command;
        }
    }
}
