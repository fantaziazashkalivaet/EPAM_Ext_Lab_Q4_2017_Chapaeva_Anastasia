namespace Task02
{
    using System;

    public class Triangle
    {
        private double a;
        private double b;
        private double c;
        private double perimeter;
        private double area;

        public Triangle()
        {
            a = 1;//todo pn хардкод
            b = 1;//todo pn хардкод
			c = 1;//todo pn хардкод
			CalculatePerimetr();
            CalculateArea();
        }

        public Triangle(double a, double b, double c)
        {
            if (SideCheck(a, b, c) && PositivCheck(a) && PositivCheck(b) && PositivCheck(c))
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }
            else
            {
                this.a = 0;
                this.b = 0;
                this.c = 0;
            }

            CalculatePerimetr();
            CalculateArea();
        }

        public double A
        {
            get
            {
                return a;
            }
        }

        public double B
        {
            get
            {
                return b;
            }
        }

        public double C
        {
            get
            {
                return c;
            }
        }

        public double Perimeter
        {
            get
            {
                return perimeter;
            }
        }

        public double Area
        {
            get
            {
                return area;
            }
        }

        private void CalculatePerimetr()
        {
            perimeter = a + b + c;
        }

        private void CalculateArea()
        {
            var p = perimeter / 2;
            area = Math.Sqrt(p * (p - a) * (p - b) * (p - c));
        }

        private bool SideCheck(double a, double b, double c)
        {
            return (a + b) > c && (a + c) > b && (b + c) > a;
        }

        private bool PositivCheck(double x)
        {
            return x > 0;
        }
    }
}
