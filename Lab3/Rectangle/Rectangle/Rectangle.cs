using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Rectangle
{
    [Serializable]
    public class Rectangle : Polygon.Polygon
    {
        public Rectangle() {}

        public Rectangle(Point point1, Point point2)
        {
            points.Add(point1);
            points.Add(new Point(point2.X, point1.Y));
            points.Add(point2);
            points.Add(new Point(point1.X, point2.Y));
            edgeAmount = 4;
        }
    }

    public class RectCanvas : BaseShape.BaseCanvas{
        public RectCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList) : base(pictureBox, g, color, shapesList) {}

        private Point topLeft;

        public override void handleClick(MouseEventArgs e) {
            if (!isDrawing) 
            {
                topLeft = new Point(e.X, e.Y);
                isDrawing = true;
            }
            else {
                Rectangle rect = new Rectangle(topLeft, new Point(e.X, e.Y));
                rect.SetColor(color);
                shapesList.Add(rect);
                rect.Draw(g);

                isDrawing = false;
        }
    }

    public override void handleMove(MouseEventArgs e) {
        if (isDrawing) {
            g.Clear(Color.White);
            shapesList.draw(g);

            Rectangle rect = new Rectangle(topLeft, new Point(e.X, e.Y));
            rect.SetColor(color);
            rect.Draw(g);
        }
    }
}
}
