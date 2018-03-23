using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab6
{
    public partial class OptionsDialog : Form
    {
        public int PenColor;
        public int FillColor;   
        public int PenWidth;   
        public bool Fill;     //filled or not  
        public bool Outline;  //outlined or not

        public OptionsDialog()
        {
            InitializeComponent();
            listBox1.SelectedIndex = 0;     //Pen color initialized to be black
            listBox2.SelectedIndex = 0;     //Fill color initialized to be white
            listBox3.SelectedIndex = 0;     //Pen width initialized to be 1
            checkBox1.Checked = false;      //not filled by default
            checkBox2.Checked = true;       //outlined by default
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)   //Fill
        {
            Fill = checkBox1.Checked;

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)   //Outline
        {
            Outline = checkBox2.Checked;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            PenColor = listBox1.SelectedIndex;
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillColor = listBox2.SelectedIndex;
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            PenWidth = listBox3.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Fill = checkBox1.Checked;
            Outline = checkBox2.Checked;
            PenColor = listBox1.SelectedIndex;
            FillColor = listBox2.SelectedIndex;
            PenWidth = listBox3.SelectedIndex;
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
