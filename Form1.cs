using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Lab6
{
    public partial class Form1 : Form
    {
        public ArrayList coordinates = new ArrayList();     
        bool isClick;
        public int Pencolor;
        public int Fillcolor;
        public int Penwidth;
        public bool Fill;
        public bool Outline;
        public Point firstpoint;

        public Form1()
        {
            InitializeComponent();
            this.Text = "Lab 6 by Shuxian Zhang";
            isClick = false;
            Pencolor = 0;       //default-black
            Fillcolor = 0;      //default-white
            Penwidth = 0;       //default-1
            Fill = false;       //not filled
            Outline = true;     //outline
            radioButton1.Checked = true;    //line
            
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OptionsDialog OptDia = new OptionsDialog();
            OptDia.ShowDialog();
            if (OptDia.DialogResult==DialogResult.OK)
            {
                Pencolor = OptDia.PenColor;
                Fillcolor = OptDia.FillColor;
                Penwidth = OptDia.PenWidth;
                Fill = OptDia.Fill;
                Outline = OptDia.Outline;
            }
            this.Refresh();
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            Point secondpoint = new Point(e.X, e.Y);

            if (isClick)                        //if it is the second click
            {
                bool errorcondition = ((Fill == false) && (Outline == false));
                if (radioButton2.Checked)       //draw rectangle
                {
                    if (errorcondition)      //both Fill and Outline are not checked
                    {
                        MessageBox.Show("Fill and or outline must be checked.");
                        isClick = false; 
                        this.Refresh();
                    }
                    else
                    { 
                        DrawRectangle newrectangle = new DrawRectangle(firstpoint, secondpoint, Pencolor, Penwidth, Fillcolor, Fill, Outline);
                        coordinates.Add(newrectangle);
                        isClick = false;
                        this.Refresh();
                    }
                }
                else if (radioButton3.Checked)  //draw ellipse
                {
                    if (errorcondition)      //both Fill and Outline are not checked
                    {
                        MessageBox.Show("Fill and or outline must be checked.");
                        isClick = false;
                        this.Refresh();
                    }
                    else
                    {
                        DrawEllipse newellipse = new DrawEllipse(firstpoint, secondpoint, Pencolor, Penwidth, Fillcolor, Fill, Outline);
                        coordinates.Add(newellipse);
                        isClick = false;
                        this.Refresh();
                    }
                }
                else                            //draw line
                {
                    DrawLine newline = new DrawLine(firstpoint, secondpoint, Pencolor, Penwidth + 1);
                    coordinates.Add(newline);
                    isClick = false;
                    this.Refresh();
                }
            }
            else       //if it is the first click
            {
                firstpoint = secondpoint;
                isClick = true;
            }
            return;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = panel2.CreateGraphics();
            foreach (MyDraw mydrawobject in coordinates)
            {
                mydrawobject.Draw(g);
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)   //File->Clear
        {
            coordinates.Clear();
            this.Refresh();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)    //File->Exit
        {
            this.Close();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)    //Edit->Undo
        {
            if (coordinates.Count > 0)
            {
                coordinates.RemoveAt(coordinates.Count - 1);
            }
            this.Refresh();
        }
    }

    public class MyDraw
    {
        public Color PenColor;  
        public int PenWidth;

        public MyDraw(int pencolor,int penwidth)
        {
            PenWidth = penwidth;
            if (pencolor==1)
            {
                PenColor = Color.FromName("Red");
            }
            else if (pencolor==2)
            {
                PenColor = Color.FromName("Blue");
            }
            else if (pencolor==3)
            {
                PenColor = Color.FromName("Green");
            }
            else
            {
                PenColor = Color.FromName("Black");
            }
        }

        virtual public void Draw(Graphics g)
        {
            return;
        }
    }

    public class DrawLine:MyDraw
    {
        public Point p1;
        public Point p2;

        public DrawLine(Point p1_1,Point p2_2,int pencolor,int penwidth ):base(pencolor,penwidth)
        {
            p1 = p1_1;
            p2 = p2_2;
        }

        public override void Draw(Graphics g)
        {
            Pen drawpen = new Pen(PenColor, PenWidth);
            g.DrawLine(drawpen, p1, p2);
            return;
        }
    }

    public class DrawRectangle : MyDraw
    {
        public int x;
        public int y;
        public int w;   //width
        public int h;   //height
        public Color FillColor;
        public bool Fill;
        public bool Outline;

        public DrawRectangle(Point p1_1, Point p2_2, int pencolor, int penwidth, int fillcolor, bool fill, bool outline) : base(pencolor, penwidth)
        {
            Fill = fill;
            Outline = outline;

            if (p1_1.X<p2_2.X)     //the first click is left to the second click
            {
                x = p1_1.X;         //the upperleft-x coordinate is the first click's
                w = p2_2.X - p1_1.X;
            }
            else
            {
                x = p2_2.X;
                w = p1_1.X - p2_2.X;
            }

            if (p1_1.Y<p2_2.Y)     //the first click is up to the second click
            {
                y = p1_1.Y;         //the upperleft-y coordinate is the first click's
                h = p2_2.Y - p1_1.Y;
            }
            else
            {
                y = p2_2.Y;
                h = p1_1.Y - p2_2.Y;
            }

            if (fillcolor == 1)
            {
                FillColor = Color.FromName("Black");
            }
            else if (fillcolor == 2)
            {
                FillColor = Color.FromName("Red");
            }
            else if (fillcolor == 3)
            {
                FillColor = Color.FromName("Blue");
            }
            else if (fillcolor == 4)
            {
                FillColor = Color.FromName("Green");
            }
            else
            {
                FillColor = Color.FromName("White");
            }
        }

        public override void Draw(Graphics g)
        {
            Pen outlinepen = new Pen(PenColor, PenWidth);
            Brush fillbrush = new SolidBrush(FillColor);

            if (Fill)
            {
                g.FillRectangle(fillbrush, x+PenWidth/2, y+PenWidth/2, w, h);
            }
            if (Outline)
            {
                g.DrawRectangle(outlinepen, x, y, w, h);
            }

            return;
        }
    }

    public class DrawEllipse : MyDraw
    {
        public int x;
        public int y;
        public int w;   //width
        public int h;   //height
        public Color FillColor;
        public bool Fill;
        public bool Outline;

        public DrawEllipse(Point p1_1,Point p2_2, int pencolor, int penwidth, int fillcolor, bool fill, bool outline) : base(pencolor, penwidth)
        {
            Fill = fill;
            Outline = outline;

            if (p1_1.X < p2_2.X)        //the first click is left to the second click
            {
                x = p1_1.X;             //the upperleft-x coordinate is the first click's
                w = p2_2.X - p1_1.X;    //width is p2x-p1x
            }
            else
            {
                x = p2_2.X;
                w = p1_1.X - p2_2.X;
            }

            if (p1_1.Y < p2_2.Y)       //the first click is up to the second click
            {
                y = p1_1.Y;             //the upperleft-y coordinate is the first click's
                h = p2_2.Y - p1_1.Y;
            }
            else
            {
                y = p2_2.Y;
                h = p1_1.Y - p2_2.Y;
            }

            if (fillcolor == 1)
            {
                FillColor = Color.FromName("Black");
            }
            else if (fillcolor == 2)
            {
                FillColor = Color.FromName("Red");
            }
            else if (fillcolor == 3)
            {
                FillColor = Color.FromName("Blue");
            }
            else if (fillcolor == 4)
            {
                FillColor = Color.FromName("Green");
            }
            else
            {
                FillColor = Color.FromName("White");
            }
        }

        public override void Draw(Graphics g)
        {
            Pen outlinepen = new Pen(PenColor, PenWidth);
            Brush fillbrush = new SolidBrush(FillColor);

            if (Fill)
            {
                g.FillEllipse(fillbrush, x, y, w, h);
            }

            if (Outline)
            {
                g.DrawEllipse(outlinepen, x, y, w, h);
            }
            return;
        }
    }
}
