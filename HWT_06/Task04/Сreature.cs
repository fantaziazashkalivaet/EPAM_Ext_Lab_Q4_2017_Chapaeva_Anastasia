namespace Task04
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class Сreature : Point
    {
        private const int Step = 1;

        public void MoveRight(int widthMax)
        {
            if (X <= widthMax - Step)
            {
                X += Step;
            }
        }

        public void MoveLeft(int widthMin)
        {
            if (X >= widthMin + Step)
            {
                X -= Step;
            }
        }

        public void MoveUp(int heightMax)
        {
            if (Y <= heightMax - Step)
            {
                Y += Step;
            }
        }

        public void MoveDown(int heightMin)
        {
            if (Y <= heightMin + Step)
            {
                Y -= Step;
            }
        }

        public void Move(Side side, TypePoint[,] map)
        {
            switch (side)
            {
                case Side.left:
                    {
                        if (X <= map.GetLength(2) - 1)
                        {
                            if (map[X, Y] == TypePoint.obctacle || map[X, Y] == TypePoint.bonus)
                            {
                                // ??
                            }
                        }

                        break;
                    }

                case Side.rigth:
                    {
                        // smth
                        break;
                    }

                case Side.up:
                    {
                        // smth
                        break;
                    }

                case Side.down:
                    {
                        // smth
                        break;
                    }
            }
        }
    }
}
