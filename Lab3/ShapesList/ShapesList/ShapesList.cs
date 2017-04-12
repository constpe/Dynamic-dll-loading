using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ShapesList
{
    [Serializable]
    public class ShapesList
    {
        public ShapesList() {}

        public List<BaseShape.BaseShape> shapesList = new List<BaseShape.BaseShape>();

        public void Add(BaseShape.BaseShape shape) 
        {
            shapesList.Add(shape);
        }

        public void Remove(int i) 
        {
            shapesList.Remove(shapesList[i]);
        }

        public int Size() 
        {
            return shapesList.Count;
        }

        public void Clear() 
        {
            shapesList.Clear();
        }

        public void draw(Graphics g) 
        {
            foreach (BaseShape.BaseShape shape in shapesList)
                shape.Draw(g);
        }
    }
}
