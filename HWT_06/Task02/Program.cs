namespace Task02
{
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            var a = new Ring();
            Console.WriteLine("x={0}, y={1}, cir={2}, area={3}, r={4}, inR={5}, ringCir={6}, ringArea={7}", a.CentreX, a.CentreY, a.Circumference, a.Area, a.Radius, a.InnerRadius, a.RingCircumference, a.RingArea);
            a = new Ring(0, 7, 3, 1);
            Console.WriteLine("x={0}, y={1}, cir={2}, area={3}, r={4}, inR={5}, ringCir={6}, ringArea={7}", a.CentreX, a.CentreY, a.Circumference, a.Area, a.Radius, a.InnerRadius, a.RingCircumference, a.RingArea);
        }
    }
}
