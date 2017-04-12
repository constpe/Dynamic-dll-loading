using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Square
{
    [Serializable]
    public class Square : Rectangle.Rectangle
    {
        public Square() {}

        public Square(Point point1, int side)
        {
            points.Add(point1);
            points.Add(new Point(point1.X + side, point1.Y));
            points.Add(new Point(point1.X + side, point1.Y + side));
            points.Add(new Point(point1.X, point1.Y + side));
            edgeAmount = 4;
        }
    }

    public class SquareCanvas : BaseShape.BaseCanvas
    {
        public SquareCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList) : base(pictureBox, g, color, shapesList)
        {
            isDrawing = false;
        }

        private Point topLeft;

        public override void handleClick(MouseEventArgs e)
        {
            if (!isDrawing)
            {
                topLeft = new Point(e.X, e.Y);
                isDrawing = true;
            }
            else
            {
                Square square = new Square(topLeft, e.X - topLeft.X);
                square.SetColor(color);
                shapesList.Add(square);
                square.Draw(g);

                isDrawing = false;
            }
        }

        public override void handleMove(MouseEventArgs e)
        {
            if (isDrawing)
            {
                g.Clear(Color.White);
                shapesList.draw(g);

                Square square = new Square(topLeft, e.X - topLeft.X);
                square.SetColor(color);
                square.Draw(g);
            }
        }
    }
}
