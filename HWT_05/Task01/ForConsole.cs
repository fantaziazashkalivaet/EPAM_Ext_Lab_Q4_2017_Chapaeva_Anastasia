namespace Task01
{
    using System;
    using Lib;

    public class ForConsole
    {
        public static void PrintRound(Round round)
        {
            Console.WriteLine("\nRound with centre in ({0}, {1}) have:", round.CentreX, round.CentreY);
            Console.WriteLine("\tcircumference = {0},", round.Circumference);
            Console.WriteLine("\tarea = {0}.", round.Area);
        }
    }
}
