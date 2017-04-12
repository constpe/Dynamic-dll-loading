using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Triangle
{
    [Serializable]
    public class Triangle : Polygon.Polygon
    {
        public Triangle(Point point1, Point point2, Point point3)
        {
            points.Add(point1);
            points.Add(point2);
            points.Add(point3);
            edgeAmount = 3;
        }
    }

    public class TriangleCanvas : BaseShape.BaseCanvas {
        public TriangleCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList) : base(pictureBox, g, color, shapesList) {}

        private Point point1;
        private Point point2;
        private Point point3;
        private Point currPoint;
        private int linesAmount = 0;

        public override void handleClick(MouseEventArgs e) {
            if (!isDrawing) {
                point1 = new Point(e.X, e.Y);
                currPoint = point1;
                linesAmount += 1;
                isDrawing = true;
            }
            else if (linesAmount == 1){
                point2 = new Point(e.X, e.Y);
                currPoint = point2;
                Line.Line line = new Line.Line(point1, point2);
                line.SetColor(color);
                line.Draw(g);
                shapesList.Add(line);
                linesAmount += 1;
            }
            else {
                shapesList.Remove(shapesList.Size() - 1);
                linesAmount = 0;
                isDrawing = false;

                point3 = new Point(e.X, e.Y);
                Triangle triangle = new Triangle(point1, point2, point3);
                triangle.SetColor(color);
                triangle.Draw(g);

                shapesList.Add(triangle);
            }
        }

    public override void handleMove(MouseEventArgs e) {
        if (isDrawing) {
            g.Clear(Color.White);
            shapesList.draw(g);

            Point nextPoint = new Point(e.X, e.Y);
            Line.Line line = new Line.Line(currPoint, nextPoint);
            line.SetColor(color);
            line.Draw(g);
        }
    }
}
}
