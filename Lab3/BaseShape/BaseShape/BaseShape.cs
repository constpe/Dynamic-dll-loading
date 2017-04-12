using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace BaseShape
{
    [Serializable
    ]public abstract class BaseShape
    {
        public Color color;

        public void SetColor(Color color)
        {
            this.color = color;
        }

        public abstract void Draw(Graphics g);
    }

    public abstract class BaseCanvas
    {
        public BaseCanvas(PictureBox pictureBox, Graphics g, Color color, ShapesList.ShapesList shapesList)
        {
            this.pictureBox = pictureBox;
            this.g = g;
            this.color = color;
            this.shapesList = shapesList;
            isDrawing = false;
        }

        internal protected PictureBox pictureBox;
        internal protected Graphics g;
        internal protected Color color;
        internal protected ShapesList.ShapesList shapesList;
        internal protected bool isDrawing;

        public abstract void handleClick(MouseEventArgs e);

        public abstract void handleMove(MouseEventArgs e);

        public void changeColor(Color color)
        {
            this.color = color;
        }
    }
}
