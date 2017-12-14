namespace Task03
{
    public class Rectangle : Figure
    {
        private double width;
        private double height;

        public Rectangle()
        {
            CentreX = 0;//todo pn хардкод
			CentreY = 0;//todo pn хардкод
			Width = 1;//todo pn хардкод
            Height = 1;//todo pn хардкод
		}

        public Rectangle(double centreX, double centreY, double width, double height)
        {
            CentreX = centreX;
            CentreY = centreY;
            Width = width;
            Height = height;
        }

        public double Width
        {
            get
            {
                return width;
            }

            set
            {
                if (value > 0)
                {
                    width = value;
                }
                else
                {
                    width = 1;//todo pn хардкод
				}
            }
        }

        public double Height
        {
            get
            {
                return height;
            }

            set
            {
                if (value > 0)
                {
                    height = value;
                }
                else
                {
                    height = 1;//todo pn хардкод
				}
            }
        }

        public override FigureType ReturnType()
        {
            return FigureType.Rectangle;
        }

        public override string Info()
        {
            return string.Format("{0}: centre = ({1}, {2}), width = {3}, height = {4}", this.ReturnType(), CentreX, CentreY, Width, Height);
        }
    }
}
