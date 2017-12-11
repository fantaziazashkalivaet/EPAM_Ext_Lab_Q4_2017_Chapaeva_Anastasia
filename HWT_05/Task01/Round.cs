namespace Task01
{
    using System;

    public class Round
    {
        private double centreX;
        private double centreY;
        private double radius;
        private double circumference;
        private double area;

        public Round()
        {
            centreX = 1;
            centreY = 1;
            radius = 1;
            CalculateCircumference();
            CalculateArea();
        }

        public Round(double centreX, double centreY, double radius)
        {
            if (radius >= 0)
            {
                this.centreX = centreX;
                this.centreY = centreY;
                this.radius = radius;
            }
            else
            {
                this.centreX = 1;
                this.centreY = 1;
                this.radius = 1;
            }

            CalculateCircumference();
            CalculateArea();
        }

        public double CentreX
        {
            get
            {
                return centreX;
            }
        }

        public double CentreY
        {
            get
            {
                return centreY;
            }
        }

        public double Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
                CalculateCircumference();
                CalculateArea();
            }
        }

        public double Circumference
        {
            get
            {
                return circumference;
            }
        }

        public double Area
        {
            get
            {
                return area;
            }
        }

        private void CalculateCircumference()
        {
            circumference = 2 * Math.PI * radius;
        }

        private void CalculateArea()
        {
            area = Math.Pow(radius, 2) * Math.PI;
        }
    }
}
