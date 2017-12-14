namespace Task03
{
    using System;

    public class ForConsole
    {
        public static void PrintMenu()
        {
            Console.WriteLine("Enter a number to create a figure or end a program:");
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Line, FigureType.Line);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Rectangle, FigureType.Rectangle);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Circle, FigureType.Circle);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Round, FigureType.Round);
            Console.WriteLine("\t{0}: create a {1};", (int)FigureType.Ring, FigureType.Ring);
            Console.WriteLine("\t0: exit.");
        }

        public static bool CheckCommand()
        {
            Random r = new Random();
            var command = SetCommand();
            bool exit = false;

            switch (command)
            {
                case (int)FigureType.Line:
                    {
                        var figure = new Line(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        Console.WriteLine("{0}: centre = ({1}, {2}), length = {3}",
                            figure.ReturnType(),
                            figure.CentreX,
                            figure.CentreY,
                            figure.Length);
                        break;
                    }

                case (int)FigureType.Rectangle:
                    {
                        var figure = new Rectangle(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        Console.WriteLine("{0}: centre = ({1}, {2}), width = {3}, height = {4}",
                            figure.ReturnType(), 
                            figure.CentreX, 
                            figure.CentreY, 
                            figure.Width, 
                            figure.Height);
                        break;
                    }

                case (int)FigureType.Circle:
                    {
                        var figure = new Circle(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        Console.WriteLine("{0}: centre = ({1}, {2}), radius = {3}",
                            figure.ReturnType(),
                            figure.CentreX,
                            figure.CentreY,
                            figure.Radius);
                        break;
                    }

                case (int)FigureType.Round:
                    {
                        var figure = new Round(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        Console.WriteLine("{0}: centre = ({1}, {2}), radius = {3}",
                            figure.ReturnType(),
                            figure.CentreX,
                            figure.CentreY,
                            figure.Radius);
                        break;
                    }

                case (int)FigureType.Ring:
                    {
                        var figure = new Ring(r.Next(1, 100), r.Next(1, 100), r.Next(1, 100), r.Next(1, 100));
                        Console.WriteLine("{0}: centre = ({1}, {2}), outer radius = {3}, inner radius = {4}",
                            figure.ReturnType(),
                            figure.CentreX,
                            figure.CentreY,
                            figure.Radius,
                            figure.OuterRadius);
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
                        exit = CheckCommand();
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
