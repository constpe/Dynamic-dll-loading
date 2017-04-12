using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Point
{
    public class Point : BaseShape.BaseShape
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        private int x;
        private int y;

        public int getX()
        {
            return this.x;
        }

        public int getY()
        {
            return this.y;
        }

        public override void Draw(Graphics g)
        {
            g.DrawLine(new Pen(color, 2), new System.Drawing.Point(this.x, this.y), new System.Drawing.Point(this.x, this.y));
        }
    }
}
