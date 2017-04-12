using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Polygon
{
    [Serializable]
    public class Polygon : BaseShape.BaseShape
    {
        public Polygon() { }

        public Polygon(Point point)
        {
            points.Add(point);
            edgeAmount = 1;
        }

        public List<Point> points = new List<Point>();
        public int edgeAmount;

        public void Add(Point point)
        {
            points.Add(point);
            edgeAmount++;
        }

        public override void Draw(Graphics g)
        {
            Point[] vertex = new Point[points.Count];

            for (int i = 0; i < points.Count; i++)
            {
                vertex[i] = points[i];
            }

            g.DrawPolygon(new Pen(color, 2), vertex);
        }
    }

    public class PolygonCanvas : BaseShape.BaseCanvas
    {
        public PolygonCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList)
            : base(pictureBox, g, color, shapesList)
        {
        }

        private Point startPoint;
        private Point currPoint;
        private Point nextPoint;
        private Polygon polygon;
        private int edgesAmount;

        public override void handleClick(MouseEventArgs e)
        {
            if (!isDrawing && e.Button == MouseButtons.Left)
            {
                startPoint = new Point(e.X, e.Y);
                currPoint = startPoint;

                polygon = new Polygon(startPoint);
                edgesAmount = 1;
                isDrawing = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                nextPoint = new Point(e.X, e.Y);
                Line.Line line = new Line.Line(currPoint, nextPoint);
                line.SetColor(color);
                line.Draw(g);
                shapesList.Add(line);

                currPoint = nextPoint;
                polygon.Add(currPoint);
                edgesAmount++;
            }
            else if (e.Button == MouseButtons.Right)
            {
                Line.Line line = new Line.Line(currPoint, startPoint);
                line.SetColor(color);
                line.Draw(g);

                for (int i = edgesAmount - 1; i > 0; i--)
                {
                    shapesList.Remove(shapesList.Size() - 1);
                }

                g.Clear(Color.White);
                polygon.SetColor(color);
                polygon.Draw(g);
                shapesList.Add(polygon);

                isDrawing = false;

                shapesList.draw(g);
            }
        }

        public override void handleMove(MouseEventArgs e)
        {
            if (isDrawing)
            {
                g.Clear(Color.White);
                shapesList.draw(g);

                nextPoint = new Point(e.X, e.Y);
                Line.Line line = new Line.Line(currPoint, nextPoint);
                line.SetColor(color);
                line.Draw(g);
            }
        }
    }
}
