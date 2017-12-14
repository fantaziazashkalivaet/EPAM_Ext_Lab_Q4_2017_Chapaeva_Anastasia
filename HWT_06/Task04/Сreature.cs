using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task04
{
    class Сreature
    {
        private Point position;

        public void MoveRight(int WidthMax, int step)
        {
            if (position.X <= WidthMax - step)
            {
                position.X += step;
            }
        }

        public void MoveLeft(int WidthMin, int step)
        {
            if (position.X >= WidthMin + step)
            {
                position.X -= step;
            }
        }

        public void MoveUp(int HeightMax, int step)
        {
            if (position.Y <= HeightMax - step)
            {
                position.Y += step;
            }
        }

        public void MoveDown(int HeightMin, int step)
        {
            if (position.Y <= HeightMin + step)
            {
                position.Y -= step;
            }
        }
    }
}
