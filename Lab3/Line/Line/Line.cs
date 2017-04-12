using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Line
{
    [Serializable]
    public class Line : BaseShape.BaseShape
    {
        public Line() {}

        public Line(Point point1, Point point2)
        {
            this.point1 = point1;
            this.point2 = point2;
        }

        public Point point1;
        public Point point2;

        public override void Draw(Graphics g)
        {
            g.DrawLine(new Pen(color, 2), point1, point2);
        }
    }

    public class LineCanvas : BaseShape.BaseCanvas
    {
        public LineCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList)
            : base(pictureBox, g, color, shapesList)
        {
        }

        private Point point1;
        private Point point2;

        public override void handleClick(MouseEventArgs e)
        {
            if (!isDrawing)
            {
                point1 = new Point(e.X, e.Y);
                isDrawing = true;
            }
            else
            {
                point2 = new Point(e.X, e.Y);
                Line line = new Line(point1, point2);
                line.SetColor(color);
                line.Draw(g);
                shapesList.Add(line);

                isDrawing = false;
            }
        }

        public override void handleMove(MouseEventArgs e)
        {
            if (isDrawing)
            {
                g.Clear(Color.White);
                shapesList.draw(g);

                point2 = new Point(e.X, e.Y);
                Line line = new Line(point1, point2);
                line.SetColor(color);
                line.Draw(g);
            }
        }
    }
}
