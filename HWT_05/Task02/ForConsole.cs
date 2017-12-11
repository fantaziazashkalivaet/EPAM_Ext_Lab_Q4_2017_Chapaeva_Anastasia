namespace Task02
{
    using System;

    public class ForConsole
    {
        public static void PrintTriangle(Triangle triangle)
        {
            Console.WriteLine("\nTriangle with side ({0}, {1}, {2}) have:", triangle.A, triangle.B, triangle.C);
            Console.WriteLine("\tperimetr = {0},", triangle.Perimeter);
            Console.WriteLine("\tarea = {0}.", triangle.Area);
        }
    }
}
