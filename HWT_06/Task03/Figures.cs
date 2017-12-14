namespace Task03
{
    using System.Collections.Generic;

    public class Figures
    {
        private List<Line> lines;
        private List<Rectangle> rectangles;
        private List<Circle> circles;
        private List<Round> rounds;
        private List<Ring> rings;

        public Figures()
        {
            Lines = new List<Line>();
            Rectangles = new List<Rectangle>();
            Circles = new List<Circle>();
            Rounds = new List<Round>();
            Rings = new List<Ring>();
        }

        public List<Line> Lines
        {
            get
            {
                return lines;
            }

            set
            {
                lines = value;
            }
        }

        public List<Rectangle> Rectangles
        {
            get
            {
                return rectangles;
            }

            set
            {
                rectangles = value;
            }
        }

        public List<Circle> Circles
        {
            get
            {
                return circles;
            }

            set
            {
                circles = value;
            }
        }

        public List<Round> Rounds
        {
            get
            {
                return rounds;
            }

            set
            {
                rounds = value;
            }
        }

        public List<Ring> Rings
        {
            get
            {
                return rings;
            }

            set
            {
                rings = value;
            }
        }

        public void SetFigures(Line line)
        {
            Lines.Add(line);
        }

        public void SetFigures(Rectangle rectangle)
        {
            Rectangles.Add(rectangle);
        }

        public void SetFigures(Circle circle)
        {
            Circles.Add(circle);
        }

        public void SetFigures(Round round)
        {
            Rounds.Add(round);
        }

        public void SetFigures(Ring ring)
        {
            Rings.Add(ring);
        }
    }
}
