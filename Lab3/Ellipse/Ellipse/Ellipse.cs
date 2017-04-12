using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Ellipse
{
    [Serializable]
    public class Ellipse : BaseShape.BaseShape
    {
        public Ellipse(Point center, int width, int height) 
        {
            this.topLeft = new Point(center.X - width / 2, center.Y - height / 2);
            this.width = width;
            this.height = height;
        }

        public Point topLeft;
        public int width;
        public int height;

        public override void Draw(Graphics  g) 
        {
            g.DrawEllipse(new Pen(color, 2), topLeft.X, topLeft.Y, width, height);
        }
    }

    public class EllipseCanvas : BaseShape.BaseCanvas {
        public EllipseCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList) : base(pictureBox, g, color, shapesList){}

        private Point center;
        private Point xBorder;
        private Point yBorder;
        private int width;
        private bool isBorder = false;

        public override void handleClick(MouseEventArgs e) 
        {
            if (!isDrawing) 
            {
                center = new Point(e.X, e.Y);
                isDrawing = true;
            }
            else if (!isBorder)
            {
                isBorder = true;
                xBorder = new Point(e.X, e.Y);
                width = Math.Abs((xBorder.X - center.X) * 2);
                Ellipse ellipse = new Ellipse(center, width, 0);
                ellipse.SetColor(color);
                ellipse.Draw(g);
            }
            else 
            {
                Ellipse ellipse = new Ellipse(center, width, Math.Abs((e.Y - center.Y) * 2));
                ellipse.SetColor(color);
                ellipse.Draw(g);
                shapesList.Add(ellipse);

                isDrawing = false;
                isBorder = false;
                width = 0;
            }   
        }

    public override void handleMove(MouseEventArgs e) {
        if (isDrawing && !isBorder) {
            g.Clear(Color.White);
            shapesList.draw(g);

            width = Math.Abs((e.X - center.X) * 2);
            Ellipse ellipse = new Ellipse(center, width, 0);
            ellipse.SetColor(color);
            ellipse.Draw(g);
        }
        else if (isBorder) {
            g.Clear(Color.White);
            shapesList.draw(g);

            yBorder = new Point(e.X, e.Y);
            Ellipse ellipse = new Ellipse(center, width, Math.Abs((e.Y - center.Y) * 2));
            ellipse.SetColor(color);
            ellipse.Draw(g);
        }
    }
}
}
