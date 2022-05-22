using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rectanglex
{
    public static class Bresenham
    {
        public static void Line(Point p1, Point p2, Bitmap bmp)
        {
            if (p1.X > p2.X)    // zamień p1 i p2
            {
                Point t = p1;
                p1 = p2;
                p2 = t;
            }
            bool reflectX = false;
            if (p2.Y < p1.Y)    // odbij p2 względem prostej y = p1.Y
            {
                p2.Y = 2 * p1.Y - p2.Y;
                reflectX = true;
            }

            int dx = p2.X - p1.X;
            int dy = p2.Y - p1.Y;

            int d;
            if (dx > dy)
                d = 2 * dy - dx;
            else
                d = dy - 2 * dx;

            int incrN = -2 * dx;
            int incrNE = 2 * (dy - dx);
            int incrE = 2 * dy;

            int x = p1.X, y = p1.Y;
            if (x > 0 && x < bmp.Width - 1 && y > 0 && y < bmp.Height - 1)
                bmp.SetPixel(x, y, Color.Black);
            while (x <= p2.X && y <= p2.Y)
            {
                if (dx > dy)
                {
                    x++;
                    if (d < 0)
                        d += incrE;
                    else
                    {
                        d += incrNE;
                        y++;
                    }

                }
                else
                {
                    y++;
                    if (d > 0)
                        d += incrN;
                    else
                    {
                        d += incrNE;
                        x++;
                    }
                }

                int yy = reflectX ? 2 * p1.Y - y : y;
                if (x > 0 && x < bmp.Width - 1 && yy > 0 && yy < bmp.Height - 1)
                    bmp.SetPixel(x, yy, Color.Black);
            }
        }

    }
}
