using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype1_1
{
   
    partial class RePrinterForm : Form
    {
        RePrinter rePrinter;
        string[] str;
        public RePrinterForm(string[] str)
        {
            this.str = str;
            InitializeComponent();
            rePrinter = new RePrinter(str);
            label2.Text = str[0];
            label3.Text = str[1];
            label4.Text = str[2];
            label5.Text = str[3];
            label6.Text = str[4];
            label7.Text = str[5];
            label8.Text = str[6];
            label9.Text = str[7];


        }

        private void button3_Click(object sender, EventArgs e)
        {
            rePrinter.PrintPreview();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            rePrinter.SetupPage();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            rePrinter.PrintDataGridView();
        }
    }
}
