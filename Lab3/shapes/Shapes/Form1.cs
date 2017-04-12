using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shapes
{
    public partial class MainForm : Form
    {
        Graphics g;
        BaseShape.BaseCanvas canvas;
        ShapesList.ShapesList shapesList = new ShapesList.ShapesList();
        Bitmap bmp;
        Color color;
        Button[] buttons;
        string[] dirs;
        Type type;

        void createButton(String shapePath, int prevBottom, int i)
        {
            buttons[i] = new Button();
            buttons[i].SetBounds(12, prevBottom + 6, 174, 23);
            buttons[i].Text = Path.GetFileNameWithoutExtension(shapePath);
            buttons[i].Click += Click;
            
            this.Controls.Add(buttons[i]);
        }

        public MainForm()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            bmp = new Bitmap(pictureBoxCanvas.Width, pictureBoxCanvas.Height);
            g = Graphics.FromImage(bmp);
            pictureBoxCanvas.Image = bmp;

            color = Color.Black;
            pictureBox1.BackColor = color;

            dirs = Directory.GetFiles(@"D:\Прога\ООТПиСП\Lab3\shapes\Libraries", "*.dll");
            buttons = new Button[dirs.Length];
            int prevBottom = 177;
            int i = 0;

            foreach (string dir in dirs)
            {
                createButton(dir, prevBottom, i++);
                prevBottom = prevBottom + 29;
            }
        }

        private void buttonPolygon_Click(object sender, EventArgs e)
        {
            canvas = new Polygon.PolygonCanvas(pictureBoxCanvas, g, color, shapesList);
        }

        private void pictureBoxCanvas_Click(object sender, EventArgs e)
        {
            

        }

        private void pictureBoxCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (canvas != null)
            {
                canvas.handleClick(e);
                pictureBoxCanvas.Refresh();
            }
        }

        private void pictureBoxCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (canvas != null)
            {
                canvas.handleMove(e);
                pictureBoxCanvas.Refresh();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DoubleBuffered = true;
        }

        private void buttonEllipse_Click(object sender, EventArgs e)
        {
            canvas = new Ellipse.EllipseCanvas(pictureBoxCanvas, g, color, shapesList);
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            //canvas = new Line.LineCanvas(pictureBoxCanvas, g, Color.Pink, shapesList);
        }

        private void buttonTriangle_Click(object sender, EventArgs e)
        {
            canvas = new Triangle.TriangleCanvas(pictureBoxCanvas, g, color, shapesList);
        }

        private void buttonRect_Click(object sender, EventArgs e)
        {
            shapesList.draw(g);
            canvas = new Rectangle.RectCanvas(pictureBoxCanvas, g, color, shapesList);
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            ColorDialog dialog = new ColorDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pictureBox1.BackColor = dialog.Color;
                color = dialog.Color;
                if (canvas != null)
                    canvas.changeColor(color);
            }
        }

        private void Click(object sender, EventArgs e)
        {
            string name = sender.ToString().Split(' ')[sender.ToString().Split(' ').Length - 1];
            string path = null;
            string lib = null;

            foreach (string dir in dirs)
            {
                string file = Path.GetFileNameWithoutExtension(dir);
                if (Path.GetFileNameWithoutExtension(dir) == name)
                {
                    path = dir;
                    lib = name;
                }
            }

            if (path != null)
            {
                Assembly assembly = Assembly.LoadFrom(path);
                AppDomain.CurrentDomain.Load(assembly.GetName());

                type = assembly.GetType(lib + "." + lib + "Canvas");

                dynamic myObject = Activator.CreateInstance(type, new object[] { pictureBoxCanvas, g, color, shapesList });

                canvas = myObject;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog.Filter = "JSON|*.json";
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
		    {
			    if (saveFileDialog.FileName != "")
			    {
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                        writer.Write(Newtonsoft.Json.JsonConvert.SerializeObject(shapesList, typeof(ShapesList.ShapesList), new Newtonsoft.Json.JsonSerializerSettings { TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full, TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All })); 
			    }
		    }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string[] dirs = Directory.GetFiles(@"D:\Прога\ООТПиСП\Lab3\shapes\Libraries", "*.dll");
            int i = 0;

            foreach (string dir in dirs)
            {
                Assembly assembly = Assembly.LoadFrom(dir);
                AppDomain.CurrentDomain.Load(assembly.GetName());
                i++;
            }

            openFileDialog.Filter = "JSON|*.json";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (openFileDialog.FileName != "")
                {
                    string json;
                    using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                        json = reader.ReadToEnd();
                    shapesList = Newtonsoft.Json.JsonConvert.DeserializeObject<ShapesList.ShapesList>(json, new Newtonsoft.Json.JsonSerializerSettings { Error = HandleError, TypeNameAssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Full, TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All });
                }
            }

            g.Clear(Color.White);
            canvas = null;
            shapesList.draw(g);
            pictureBoxCanvas.Refresh();
        }

        void HandleError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs eArgs)
        {
            var error = eArgs.ErrorContext.Error.Message;
            eArgs.ErrorContext.Handled = true;
            MessageBox.Show("Can't open this file, some libraries missing", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void buttonLine_Click_1(object sender, EventArgs e)
        {
            canvas = new Line.LineCanvas(pictureBoxCanvas, g, color, shapesList);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            pictureBoxCanvas.Refresh();
            shapesList.Clear();
            canvas = null;
        }
    }
}
